using Confluent.Kafka;
using INotificationManagementSystem.Configuration;
using INotificationManagementSystem.Services.Abstractions;
using INotificationManagementSystem.Services.Implementations;

namespace INotificationManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region register background service
            builder.Services.AddHostedService<NotificationBackgroundService>();
            #endregion
            
            #region register email service
            var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration")
               .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfiguration!);

            #endregion

            #region register services
            builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
            #endregion

            #region register kafka
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "kafkabroker:9092",
                GroupId = "order-consumer-group",    // Consumer group ID
                AutoOffsetReset = AutoOffsetReset.Earliest, // Start reading from the beginning if no offset is stored
                EnableAutoCommit = true
            };
            var kafkaConsumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            builder.Services.AddSingleton<IConsumer<Null,string>>(kafkaConsumer);
            #endregion

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapOpenApi();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

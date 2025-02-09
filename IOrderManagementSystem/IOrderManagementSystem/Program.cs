using Confluent.Kafka;
using IOrderManagementSystem.Domain.Context;
using IOrderManagementSystem.Repositories.Abstractions;
using IOrderManagementSystem.Repositories.Implementations;
using IOrderManagementSystem.Services.Abstractions;
using IOrderManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region register automapper
            builder.Services.AddAutoMapper(typeof(Program));
            #endregion

            #region register repositories
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            #endregion

            #region register services
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            #endregion

            #region register database
            var orderDbConnection = builder.Configuration.GetConnectionString("OrderServiceDb");
            builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(orderDbConnection));
            #endregion

            #region register kafka
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "kafkabroker:9092"
            };
            var kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();
            builder.Services.AddSingleton<IProducer<Null, string>>(kafkaProducer);
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.MapOpenApi();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #region run database migration
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
            #endregion

            app.Run();
        }
    }
}

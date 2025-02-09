using Confluent.Kafka;
using INotificationManagementSystem.Dtos;
using INotificationManagementSystem.Services.Abstractions;
using System.Text.Json;

namespace INotificationManagementSystem.Services.Implementations
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly string _topic = "order-created";
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConsumer<Null, string> _kafkaConsumer;
        public NotificationBackgroundService(IServiceScopeFactory serviceScopeFactory, IConsumer<Null, string> kafkaConsumer)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _kafkaConsumer = kafkaConsumer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => ConsumeMessagesAsync(stoppingToken), stoppingToken);
        }
        private async Task ConsumeMessagesAsync(CancellationToken stoppingToken)
        {
            _kafkaConsumer.Subscribe(_topic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = _kafkaConsumer.Consume(stoppingToken);

                    if (consumeResult == null)
                    {
                        continue;
                    }

                    await ProcessMessageAsync(consumeResult.Message.Value, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            finally
            {
                _kafkaConsumer.Close();
            }
        }

        private async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
        {
            try
            {
                var order = JsonSerializer.Deserialize<OrderNotificationMessageDto>(message);

                if (order == null)
                {
                    return;
                }

                using var scope = _serviceScopeFactory.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailSenderService>();

                var emailMessage = new Message
                {
                    Order = order,
                    To = new MimeKit.MailboxAddress("Customer", order.Email)
                };

                await emailService.SendEmail(emailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

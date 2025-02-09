using INotificationManagementSystem.Dtos;

namespace INotificationManagementSystem.Services.Abstractions
{
    public interface IEmailSenderService
    {
        Task SendEmail(Message message);
    }
}

using MimeKit;

namespace INotificationManagementSystem.Dtos
{
    public class Message
    {
        public MailboxAddress To { get; set; } = null!;
        public OrderNotificationMessageDto Order { get; set; } = null!;
    }
}

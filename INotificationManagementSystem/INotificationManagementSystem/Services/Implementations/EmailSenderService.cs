using INotificationManagementSystem.Configuration;
using INotificationManagementSystem.Dtos;
using INotificationManagementSystem.Services.Abstractions;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.Security;
using System.Text;

namespace INotificationManagementSystem.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSenderService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Notification Service", _emailConfiguration.From));
            emailMessage.To.Add(new MailboxAddress("Customer", message.To.Address));
            emailMessage.Subject = "Order Created Notification";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = GenerateEmailContent(message.Order) };
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Add certificate validation callback before connecting
                    client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
                    {
                        // Option 1: Validate against specific certificate details
                        if (certificate != null && errors == SslPolicyErrors.RemoteCertificateChainErrors)
                        {
                            // Add specific validation logic here
                            return true; // Only if certificate meets your criteria
                        }

                        // Option 2: Default validation
                        return errors == SslPolicyErrors.None;
                    };

                    await client.ConnectAsync(
                        _emailConfiguration.SmtpServer,
                        _emailConfiguration.Port,
                        SecureSocketOptions.SslOnConnect
                    );

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception)
                {
                    throw; // Preserve the stack trace
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        private string GenerateEmailContent(OrderNotificationMessageDto order)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Hello,</h1>");
            sb.AppendLine("<p>Your order has been created successfully!</p>");
            sb.AppendLine("<p>Here are the details of your order:</p>");
            sb.AppendLine("<ul>");

            foreach (var product in order.Products)
            {
                sb.AppendLine($"<li>{product.Name} - {product.Quantity} piece</li>");
            }

            sb.AppendLine("</ul>");
            sb.AppendLine($"<p><strong>Total Price:</strong> ${order.TotalPrice:F2}</p>");
            sb.AppendLine($"<p><strong>Expected Delivery Date:</strong> {order.ArrivalDate:MMMM dd, yyyy}</p>");
            sb.AppendLine("<p>Thank you for shopping with us!</p>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}

using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using vm_rental.ViewModels;

namespace vm_rental.Email_Setup
{
    public interface IEmailService
    {
        void Send(ClientViewModel clienVm);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
    public class EmailService: IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration) { _emailConfiguration = emailConfiguration; }

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };

                    emailMessage.ToAddress.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddress.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }
                return emails;
            }

        }

        public async void Send(ClientViewModel clientVM)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(_emailConfiguration.SenderUsername, _emailConfiguration.SenderEmail);
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress(clientVM.FirstName, clientVM.Email);
            message.To.Add(to);

            message.Subject = _emailConfiguration.ConfirmEmailSubject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<b>Hello Ivan Georgiev.</b>";


           message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}


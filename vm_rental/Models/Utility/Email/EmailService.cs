using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorEngine;
using RazorEngine.Templating;

namespace vm_rental.Models.Utility.Email
{
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

        public async void Send(string receiverEmail,string receiverName)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(_emailConfiguration.SenderUsername, _emailConfiguration.SenderEmail);
            MailboxAddress to = new MailboxAddress(receiverEmail, receiverName);

            message.From.Add(from);
            message.To.Add(to);
            message.Subject = _emailConfiguration.ConfirmEmailSubject;

            BodyBuilder bodyBuilder = new BodyBuilder();

            string path = Directory.GetCurrentDirectory() + "\\Views\\EmailTemplate\\EmailTemplateView.cshtml";
            string file = File.ReadAllText(path);

            string result = Engine.Razor.RunCompile(file, "templateKey", null, new { Name = receiverName });

            bodyBuilder.HtmlBody = result;

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


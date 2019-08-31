using MimeKit;
using MailKit.Net.Smtp;
using RazorEngine;
using RazorEngine.Templating;
using vm_rental.Utility.Helpers;
 
namespace vm_rental.Utility.Services.Email.Mailkit
{
  public class MailkitService: IEmailService
    {
        private readonly IEmailConfiguration emailConfig;

        public MailkitService(IEmailConfiguration emailConfig) { this.emailConfig = emailConfig; }

        public async void SendEmailAsync(string email, string name, string text, EmailSubject emailSubject)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(emailConfig.SenderUsername, emailConfig.SenderEmail);
            MailboxAddress to   = new MailboxAddress(name, email);

            message.From.Add(from);
            message.To.Add(to);
            message.Subject = emailSubject.GetSubjectName();
     
            string emailTemplatePath = PathHelper.FromRoot("\\Views\\Email\\EmailTemplate.cshtml");
            string emailTemplateFile = System.IO.File.ReadAllText(emailTemplatePath);

            var razorEngine = Engine.Razor;

            string emailTemplateHTML = razorEngine.RunCompile(emailTemplateFile, "templateKey", null, new
            {
              EmailSubject = message.Subject,
              ReceiverName = name,
              URL = text
            });

            BodyBuilder bodyBuilder = new BodyBuilder()
            {
              HtmlBody = emailTemplateHTML
            };

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();

            await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailConfig.SmtpUsername, emailConfig.SmtpPassword);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);

            client.Dispose();
        }
        public async void SendEmailAsync(string receiverEmail, string receiverName, EmailSubject emailSubject)
        {
          MimeMessage message = new MimeMessage();

          MailboxAddress from = new MailboxAddress(emailConfig.SenderUsername, emailConfig.SenderEmail);
          MailboxAddress to = new MailboxAddress(receiverEmail, receiverName);

          message.From.Add(from);
          message.To.Add(to);
          message.Subject = emailSubject.GetSubjectName();

          string emailTemplatePath = PathHelper.FromRoot("\\Views\\EmailTemplate\\EmailTemplateView.cshtml");
          string emailTemplateFile = System.IO.File.ReadAllText(emailTemplatePath);


          string emailTemplateHTML = Engine.Razor.RunCompile(emailTemplateFile, "templateKey", null, new
          {
            EmailSubject = message.Subject,
            ReceiverName = receiverName
          });

          BodyBuilder bodyBuilder = new BodyBuilder()
          {
            HtmlBody = emailTemplateHTML
          };

          message.Body = bodyBuilder.ToMessageBody();

          SmtpClient client = new SmtpClient();

          await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

          await client.AuthenticateAsync(emailConfig.SmtpUsername, emailConfig.SmtpPassword);

          await client.SendAsync(message);

          await client.DisconnectAsync(true);

          client.Dispose();
        }
  }
}


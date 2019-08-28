using MimeKit;
using System.Collections.Generic;
using System.Text;

namespace vm_rental.Utility.Services.Email
{
    public class EmailMessage
    {
        public EmailMessage(MailboxAddress from, MailboxAddress to, string text, EmailSubject emailSubject)
        {
          FromEmailAddress = from;
          ToEmailAddress = to;
          Subject = emailSubject;
        }
        
        public MimeMessage Message { get; set; }
        public MailboxAddress FromEmailAddress { get; private set; }
        public MailboxAddress ToEmailAddress { get; private set; }
        public EmailSubject Subject { get; private set; }
        public string Body { get; private set; }
    }
}

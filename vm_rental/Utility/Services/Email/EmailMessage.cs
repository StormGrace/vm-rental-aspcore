using System.Collections.Generic;

namespace vm_rental.Utility.Services.Email
{
    public class EmailMessage
    {
        public EmailMessage(){
            ToAddress = new List<EmailAddress>();
            FromAddress = new List<EmailAddress>();
            }

        public List<EmailAddress> ToAddress { get; set; }
        public List<EmailAddress> FromAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

    }
}

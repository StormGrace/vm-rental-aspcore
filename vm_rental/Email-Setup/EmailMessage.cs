using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Email_Setup
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

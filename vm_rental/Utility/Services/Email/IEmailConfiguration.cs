
namespace vm_rental.Utility.Services.Email
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
        string SenderEmail { get; set; }
        string SenderUsername { get; set; }
        string ConfirmEmailSubject { get; set; }
    }


    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SenderEmail { get; set; }
        public string SenderUsername { get; set; }
        public string ConfirmEmailSubject { get; set; }
    }

}

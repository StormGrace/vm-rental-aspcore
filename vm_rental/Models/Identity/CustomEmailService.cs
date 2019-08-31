using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using vm_rental.Utility.Services.Email;

namespace vm_rental.Models.Identity
{
  public class CustomEmailService : IEmailSender
  {
    private readonly IEmailService emailService;

    public CustomEmailService(IEmailService emailService)
    {
      this.emailService = emailService;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      return Task.Run(() =>
      {
        emailService.SendEmailAsync(email, subject, EmailSubject.EmailConfirmationSubject);
      });
    }
  }
}

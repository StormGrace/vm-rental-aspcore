using System.Collections.Generic;

namespace vm_rental.Utility.Services.Email
{
  public interface IEmailService
  {
    void SendEmailAsync(string email, string name, EmailSubject subject);
    void SendEmailAsync(string email, string name, string text, EmailSubject subject);
  }
}

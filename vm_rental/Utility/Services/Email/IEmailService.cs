using System.Collections.Generic;

namespace vm_rental.Utility.Services.Email
{
  public interface IEmailService
  {
    void SendEmailAsync(string receiverEmail, string receiverName);
  }
}

using System.Collections.Generic;

namespace vm_rental.Models.Utility.Services.Email
{
  public interface IEmailService
  {
    void Send(string receiverEmail, string receiverName);
  }
}

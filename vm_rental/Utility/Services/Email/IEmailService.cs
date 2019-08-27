using System.Collections.Generic;

namespace vm_rental.Utility.Services.Email
{
  public interface IEmailService
  {
    void Send(string receiverEmail, string receiverName);
  }
}

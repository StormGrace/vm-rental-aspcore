using System.Collections.Generic;

namespace vm_rental.Models.Utility.Email
{
  public interface IEmailService
  {
    void Send(string receiverEmail, string receiverName);
    List<EmailMessage> ReceiveEmail(int maxCount = 10);
  }
}

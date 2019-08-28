using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Models
{
  public class CustomEmailConfirmationTokenProvider<TUser>: DataProtectorTokenProvider<TUser> where TUser : class
  {
    public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<EmailConfirmationTokenProviderOptions> options)
    : base(dataProtectionProvider, options)
    {

    }
  }
  public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
  {
    public EmailConfirmationTokenProviderOptions()
    {
      Name = "EmailDataProtectorTokenProvider";
      TokenLifespan = TimeSpan.FromDays(1);
    }
  }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Models
{
  public static class CustomIdentityBuilderExtensions
  {
    public static IdentityBuilder AddCustomEmailTokenProvider(this IdentityBuilder builder)
    {
      var userType = builder.UserType;
      var provider = typeof(CustomEmailConfirmationTokenProvider<>).MakeGenericType(userType);
      return builder.AddTokenProvider("CustomEmailConfirmation", provider);
    }
  }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
  public interface IUserTokenAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int UserTokenId { get; set; }
  }
  public partial class UserToken : IdentityUserToken<int>, IUserTokenAnnotations
  {
    public override bool Equals(object obj)
    {
      return obj is UserToken token &&
             LoginProvider.Equals(token.LoginProvider) &&
             Name.Equals(token.Name);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(LoginProvider, Name, Value);
    }
  }
}

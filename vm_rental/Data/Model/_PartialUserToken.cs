using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
  public interface IUserTokenAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int UserTokenId { get; set; }
  }
  public partial class UserToken  : IdentityUserToken<int>, IUserTokenAnnotations
  {

  }
}

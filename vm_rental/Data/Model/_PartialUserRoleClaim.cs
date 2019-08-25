using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
  public interface IUserRoleClaimAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int Id { get; set; }
  }
  public partial class UserRoleClaim : IdentityRoleClaim<int>, IUserRoleClaimAnnotations
  {
     
  }
}

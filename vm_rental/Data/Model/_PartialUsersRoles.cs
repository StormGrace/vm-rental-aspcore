using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
  public interface IUsersRolesAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int UsersRolesId { get; set; }
  }
  public partial class UsersRoles : IdentityUserRole<int>, IUsersRolesAnnotations
  {

  }
}

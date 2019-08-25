using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
  public interface IUserRoleAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int Id { get; set; }
  }
  public partial class UserRole : IdentityRole<int>, IUserRoleAnnotations
  {

  }
}

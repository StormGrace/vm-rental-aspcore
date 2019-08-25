using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Data.Model
{
  public interface IUserLoginAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int UserLoginId { get; set; }
  }
  public partial class UserLogin : IdentityUserLogin<int>, IUserLoginAnnotations
  {

  }
}

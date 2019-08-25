using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Data.Model
{
  public interface IUserClaimAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int Id { get; set; }
  }
  public partial class UserClaim : IdentityUserClaim<int>, IUserClaimAnnotations
  {

  }
}

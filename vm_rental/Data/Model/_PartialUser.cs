using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
  public interface IUserAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int Id { get; set; }
  }
  public partial class User : IdentityUser<int>, IUserAnnotations
  {

  }
}

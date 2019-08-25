using System;
using System.ComponentModel.DataAnnotations.Schema;

//This Class is meant to extend the functionality of it's referenced Entity Class, by protecting it from the EF Generator.
//Add the new functionality here.
namespace vm_rental.Data.Model
{
  public interface IUserHistoryAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int UserHistoryId { get; set; }
  }
  public partial class UserHistory : IUserHistoryAnnotations
  {

  }
}

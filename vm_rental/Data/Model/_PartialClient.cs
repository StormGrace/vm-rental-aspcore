using System;
using System.ComponentModel.DataAnnotations.Schema;
using vm_rental.Data.JSON;

//This Class is meant to extend the functionality of it's referenced Entity Class, by protecting it from the EF Generator.
//Add any new functionality here.
namespace vm_rental.Data.Model
{
  public interface IClientAnnotations
  {
    //Auto-Increment the ID Field on Inserts.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int ClientId { get; set; }
  }
  public partial class Client : IClientAnnotations
  {

  }
}
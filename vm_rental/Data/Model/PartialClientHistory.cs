using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

//This Class is meant to extend the functionality of it's referenced Entity Class, by protecting it from the EF Generator.
//Add the new functionality here.
namespace vm_rental.Data.Model
{
    public interface IClientHistoryAnnotations
    {
        //Auto-Increment the ID Field on Inserts.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int ClientHistoryId { get ; set; }
    }
    public partial class ClientHistory : IClientHistoryAnnotations
    {
        public ClientHistory() { }

        public ClientHistory(string firmName, string firmOwner, string firmEmail, string firmPhone, string state, string city, byte isFirm, string address = "N/A", string firmRegNumber = "N/A", string vatNumber = "N/A", string firmFax = "N/A" )
        {
            FirmName = firmName;
            FirmOwner = firmOwner;
            FirmEmail = firmEmail;
            FirmPhone = firmPhone;
            FirmRegNumber = firmRegNumber;
            VatNumber = vatNumber;
            FirmFax = firmFax;
            State = state;
            City = city;
            Address = address;
            IsFirm = isFirm;
            IsVatTaxed = 1;
            IsActive = 1;
            DateCreated = DateTime.UtcNow;
        }
    }
}

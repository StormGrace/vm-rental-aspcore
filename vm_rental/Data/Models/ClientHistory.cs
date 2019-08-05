using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ClientHistory
    {
        public int ClientHistoryId { get; set; }
        public int ClientId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string FirmName { get; set; }
        public string FirmOwner { get; set; }
        public string FirmEmail { get; set; }
        public string FirmPhone { get; set; }
        public string FirmRegNumber { get; set; }
        public string VatNumber { get; set; }
        public string FirmFax { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public byte IsVatTaxed { get; set; }
        public byte IsActive { get; set; }

        public virtual Client Client { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}

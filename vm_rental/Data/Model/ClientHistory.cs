using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class ClientHistory
    {
        public int ClientHistoryId { get; set; }
        public int ClientClientId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public byte Version { get; set; }
        public byte IsActive { get; set; }
        public string FirmName { get; set; }
        public string City { get; set; }
        public string FirmNumber { get; set; }
        public string VatNumber { get; set; }
        public string WithoutVarReason { get; set; }
        public byte IsVatTaxed { get; set; }
        public string Address { get; set; }
        public string AccPerson { get; set; }
        public string OrgPhone { get; set; }
        public string OrgEmail { get; set; }
        public string OrgFax { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}

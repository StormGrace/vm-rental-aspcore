using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class Client
    {
        public Client()
        {
            ClientDiscount = new HashSet<ClientDiscount>();
            ClientHistory = new HashSet<ClientHistory>();
            Machine = new HashSet<Machine>();
            User = new HashSet<User>();
        }

        public int ClientId { get; set; }
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
        public byte IsFirm { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual ICollection<ClientDiscount> ClientDiscount { get; set; }
        public virtual ICollection<ClientHistory> ClientHistory { get; set; }
        public virtual ICollection<Machine> Machine { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}

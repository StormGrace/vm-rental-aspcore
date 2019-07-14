using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientDiscount = new HashSet<ClientDiscount>();
            ClientOrder = new HashSet<ClientOrder>();
            Machine = new HashSet<Machine>();
            User = new HashSet<User>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PwdHash { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string FirmName { get; set; }
        public string FirmOwner { get; set; }
        public byte IsFirm { get; set; }

        public virtual ICollection<ClientDiscount> ClientDiscount { get; set; }
        public virtual ICollection<ClientOrder> ClientOrder { get; set; }
        public virtual ICollection<Machine> Machine { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}

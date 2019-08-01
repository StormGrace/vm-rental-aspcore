using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        public virtual ICollection<ClientDiscount> ClientDiscount { get; set; }
        public virtual ICollection<ClientHistory> ClientHistory { get; set; }
        public virtual ICollection<Machine> Machine { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class User
    {
        public User()
        {
            MachinesUsers = new HashSet<MachinesUsers>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PwdHash { get; set; }
        public string Email { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int ClientClientId { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual ICollection<MachinesUsers> MachinesUsers { get; set; }
    }
}

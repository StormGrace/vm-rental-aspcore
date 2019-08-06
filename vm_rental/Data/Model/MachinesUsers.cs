using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachinesUsers
    {
        public MachinesUsers()
        {
            Role = new HashSet<Role>();
        }

        public int MachinesUsersId { get; set; }
        public int UserId { get; set; }
        public int MachineId { get; set; }

        public virtual Machine Machine { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}

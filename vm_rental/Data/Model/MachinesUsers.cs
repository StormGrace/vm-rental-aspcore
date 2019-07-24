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
        public int UserUserId { get; set; }
        public int MachineMachineId { get; set; }

        public virtual Machine MachineMachine { get; set; }
        public virtual User UserUser { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class MachinesUsers
    {
        public MachinesUsers()
        {
            MachinesUserGroups = new HashSet<MachinesUserGroups>();
        }

        public int MachineUserId { get; set; }
        public int UserUserId { get; set; }
        public int MachineMachineId { get; set; }

        public virtual Machine MachineMachine { get; set; }
        public virtual User UserUser { get; set; }
        public virtual ICollection<MachinesUserGroups> MachinesUserGroups { get; set; }
    }
}

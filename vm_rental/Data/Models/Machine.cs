using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class Machine
    {
        public Machine()
        {
            MachineComponent = new HashSet<MachineComponent>();
            MachineHistory = new HashSet<MachineHistory>();
            MachinesUsers = new HashSet<MachinesUsers>();
        }

        public int MachineId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<MachineComponent> MachineComponent { get; set; }
        public virtual ICollection<MachineHistory> MachineHistory { get; set; }
        public virtual ICollection<MachinesUsers> MachinesUsers { get; set; }
    }
}

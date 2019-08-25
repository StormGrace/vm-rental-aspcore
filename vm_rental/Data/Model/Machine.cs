using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
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
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<MachineComponent> MachineComponent { get; set; }
        public virtual ICollection<MachineHistory> MachineHistory { get; set; }
        public virtual ICollection<MachinesUsers> MachinesUsers { get; set; }
    }
}

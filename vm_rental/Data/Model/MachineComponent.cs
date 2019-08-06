using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineComponent
    {
        public MachineComponent()
        {
            MachineComponentHistory = new HashSet<MachineComponentHistory>();
            OrderItems = new HashSet<OrderItems>();
        }

        public int ComponentId { get; set; }
        public int ComponentTypeId { get; set; }
        public int MachineId { get; set; }
        public decimal ActiveAmount { get; set; }

        public virtual ComponentType ComponentType { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual ICollection<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}

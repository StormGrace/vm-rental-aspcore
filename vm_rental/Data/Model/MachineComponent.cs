using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineComponent
    {
        public MachineComponent()
        {
            MachineComponentHistory = new HashSet<MachineComponentHistory>();
            OrdersItems = new HashSet<OrdersItems>();
        }

        public int ComponentId { get; set; }
        public int ComponentTypeId { get; set; }
        public int MachineId { get; set; }
        public string Name { get; set; }
        public decimal ActiveAmount { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual MachineComponentType ComponentType { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual ICollection<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual ICollection<OrdersItems> OrdersItems { get; set; }
    }
}

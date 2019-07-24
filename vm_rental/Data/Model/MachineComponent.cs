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
        public int ComponentTypeComponentTypeId { get; set; }
        public int MachineMachineId { get; set; }
        public decimal ActiveAmount { get; set; }

        public virtual ComponentType ComponentTypeComponentType { get; set; }
        public virtual Machine MachineMachine { get; set; }
        public virtual ICollection<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}

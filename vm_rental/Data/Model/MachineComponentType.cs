using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineComponentType
    {
        public MachineComponentType()
        {
            MachineComponent = new HashSet<MachineComponent>();
            MachineComponentTypeHistory = new HashSet<MachineComponentTypeHistory>();
            Product = new HashSet<Product>();
        }

        public int ComponentTypeId { get; set; }
        public byte IsSplitable { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual ICollection<MachineComponent> MachineComponent { get; set; }
        public virtual ICollection<MachineComponentTypeHistory> MachineComponentTypeHistory { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}

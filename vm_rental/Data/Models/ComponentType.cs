using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ComponentType
    {
        public ComponentType()
        {
            ComponentTypeHistory = new HashSet<ComponentTypeHistory>();
            MachineComponent = new HashSet<MachineComponent>();
            Product = new HashSet<Product>();
        }

        public int ComponentTypeId { get; set; }

        public virtual ICollection<ComponentTypeHistory> ComponentTypeHistory { get; set; }
        public virtual ICollection<MachineComponent> MachineComponent { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}

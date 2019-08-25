using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineComponentTypeHistory
    {
        public int ComponentTypeHistoryId { get; set; }
        public int ComponentTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public string Changes { get; set; }
        public byte IsSplitable { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual MachineComponentType ComponentType { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}

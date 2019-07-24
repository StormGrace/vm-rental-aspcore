using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineComponentHistory
    {
        public int ComponentHistoryId { get; set; }
        public int MachineComponentComponentId { get; set; }
        public int ProductProductId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int Version { get; set; }
        public byte IsActive { get; set; }
        public string Name { get; set; }
        public decimal AddedAmount { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MachineComponent MachineComponentComponent { get; set; }
        public virtual Product ProductProduct { get; set; }
    }
}

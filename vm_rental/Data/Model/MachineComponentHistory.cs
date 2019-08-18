using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineComponentHistory 
    {
        public int ComponentHistoryId { get; set; }
        public int MachineComponentId { get; set; }
        public int ProductId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string Name { get; set; }
        public decimal AddedAmount { get; set; }
        public byte IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual MachineComponent MachineComponent { get; set; }
        public virtual Product Product { get; set; }
    }
}

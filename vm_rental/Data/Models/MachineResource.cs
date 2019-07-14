using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class MachineResource
    {
        public MachineResource()
        {
            ResourceUsageLog = new HashSet<ResourceUsageLog>();
        }

        public int MachineResourceId { get; set; }
        public int TotalAmount { get; set; }
        public string PartitionLabel { get; set; }
        public byte IsPartition { get; set; }
        public int MachineMachineId { get; set; }
        public int ProductProductId { get; set; }

        public virtual Machine MachineMachine { get; set; }
        public virtual Product ProductProduct { get; set; }
        public virtual ICollection<ResourceUsageLog> ResourceUsageLog { get; set; }
    }
}

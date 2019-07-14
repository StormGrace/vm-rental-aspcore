using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ResourceUsageLog
    {
        public int ResourceUsageId { get; set; }
        public string AmountUsed { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal Usage { get; set; }
        public int MachineResourceMachineResourceId { get; set; }

        public virtual MachineResource MachineResourceMachineResource { get; set; }
    }
}

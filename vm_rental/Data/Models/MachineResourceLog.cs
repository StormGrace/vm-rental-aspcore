using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class MachineResourceLog
    {
        public int ResourceLogId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string UpdateType { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public int Amount { get; set; }
        public int MachineMachineId { get; set; }

        public virtual Machine MachineMachine { get; set; }
    }
}

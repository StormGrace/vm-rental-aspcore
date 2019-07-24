using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachineHistory
    {
        public int MachineHistoryId { get; set; }
        public int MachineMachineId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int Version { get; set; }
        public byte IsActive { get; set; }
        public string Name { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Machine MachineMachine { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class MachinePanelLog
    {
        public int PanelLogId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public int UserId { get; set; }
        public int MachineMachineId { get; set; }

        public virtual Machine MachineMachine { get; set; }
    }
}

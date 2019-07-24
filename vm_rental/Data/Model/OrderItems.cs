using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class OrderItems
    {
        public int RecId { get; set; }
        public int ProductProductId { get; set; }
        public int MachineComponentComponentId { get; set; }
        public int OrderOrderId { get; set; }
        public int OrderedAmount { get; set; }
        public string OrderType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? StartDateExecuted { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EndDateExecuted { get; set; }

        public virtual MachineComponent MachineComponentComponent { get; set; }
        public virtual Order OrderOrder { get; set; }
        public virtual Product ProductProduct { get; set; }
    }
}

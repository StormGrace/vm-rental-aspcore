using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public int OrderedBy { get; set; }
        public DateTimeOffset DateOrdered { get; set; }

        public virtual User OrderedByNavigation { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}

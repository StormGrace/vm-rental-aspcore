using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class Order
    {
        public Order()
        {
            OrdersItems = new HashSet<OrdersItems>();
        }

        public int OrderId { get; set; }
        public int OrderedBy { get; set; }
        public DateTimeOffset DateOrdered { get; set; }

        public virtual User OrderedByNavigation { get; set; }
        public virtual ICollection<OrdersItems> OrdersItems { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public int CreatedById { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}

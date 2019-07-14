using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class OrdersResources
    {
        public int OrderResourceId { get; set; }
        public int Quantity { get; set; }
        public int ClientOrderOrderId { get; set; }
        public int ProductProductId { get; set; }

        public virtual ClientOrder ClientOrderOrder { get; set; }
        public virtual Product ProductProduct { get; set; }
    }
}

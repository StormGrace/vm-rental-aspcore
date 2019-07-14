using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ClientOrder
    {
        public ClientOrder()
        {
            OrdersResources = new HashSet<OrdersResources>();
        }

        public int OrderId { get; set; }
        public string OrderType { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ExecutionDate { get; set; }
        public byte IsPrepaid { get; set; }
        public int ClientPaymentPaymentId { get; set; }
        public int ClientClientId { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual ClientPayment ClientPaymentPayment { get; set; }
        public virtual ICollection<OrdersResources> OrdersResources { get; set; }
    }
}

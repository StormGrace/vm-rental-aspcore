using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ClientDiscount
    {
        public int ClientDiscountId { get; set; }
        public string DiscountAmount { get; set; }
        public byte IsDiscountActive { get; set; }
        public int ProductProductId { get; set; }
        public int ClientClientId { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual Product ProductProduct { get; set; }
    }
}

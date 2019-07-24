using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class ClientDiscount
    {
        public ClientDiscount()
        {
            ClientDiscountHistory = new HashSet<ClientDiscountHistory>();
        }

        public int DiscountId { get; set; }
        public int ClientClientId { get; set; }
        public int ProductProductId { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual Product ProductProduct { get; set; }
        public virtual ICollection<ClientDiscountHistory> ClientDiscountHistory { get; set; }
    }
}

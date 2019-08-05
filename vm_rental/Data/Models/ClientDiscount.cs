using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ClientDiscount
    {
        public ClientDiscount()
        {
            ClientDiscountHistory = new HashSet<ClientDiscountHistory>();
        }

        public int DiscountId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ClientDiscountHistory> ClientDiscountHistory { get; set; }
    }
}

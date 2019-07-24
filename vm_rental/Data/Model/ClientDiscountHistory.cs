using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class ClientDiscountHistory
    {
        public int DiscountHistoryId { get; set; }
        public int ClientDiscountDiscountId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int Version { get; set; }
        public byte IsActive { get; set; }
        public decimal DiscountPercent { get; set; }

        public virtual ClientDiscount ClientDiscountDiscount { get; set; }
        public virtual User CreatedByNavigation { get; set; }
    }
}

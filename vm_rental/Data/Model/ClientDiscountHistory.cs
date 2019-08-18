using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace vm_rental.Data.Model
{
    public partial class ClientDiscountHistory
    {
        public int DiscountHistoryId { get; set; }
        public int DiscountId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public decimal DiscountPercent { get; set; }
        public byte IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ClientDiscount Discount { get; set; }
    }
}

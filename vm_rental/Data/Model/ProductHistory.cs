using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class ProductHistory
    {
        public int ProductHistoryId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductSupplier Supplier { get; set; }
    }
}

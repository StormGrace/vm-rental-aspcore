using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class ProductHistory
    {
        public int ProductHistoryId { get; set; }
        public int ProductProductId { get; set; }
        public int ProductSupplierSupplierId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int Version { get; set; }
        public byte IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Product ProductProduct { get; set; }
        public virtual ProductSupplier ProductSupplierSupplier { get; set; }
    }
}

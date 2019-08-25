using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class ProductSupplier
    {
        public ProductSupplier()
        {
            ProductHistory = new HashSet<ProductHistory>();
            ProductSupplierHistory = new HashSet<ProductSupplierHistory>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierDescription { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
        public virtual ICollection<ProductSupplierHistory> ProductSupplierHistory { get; set; }
    }
}

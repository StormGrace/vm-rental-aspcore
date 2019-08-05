using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ProductSupplier
    {
        public ProductSupplier()
        {
            ProductHistory = new HashSet<ProductHistory>();
            ProductSupplierHistory = new HashSet<ProductSupplierHistory>();
        }

        public int SupplierId { get; set; }

        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
        public virtual ICollection<ProductSupplierHistory> ProductSupplierHistory { get; set; }
    }
}

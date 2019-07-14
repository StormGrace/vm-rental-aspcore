using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ProductSupplier
    {
        public ProductSupplier()
        {
            Product = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierDescription { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}

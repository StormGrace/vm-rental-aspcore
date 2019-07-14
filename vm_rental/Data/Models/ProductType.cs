using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}

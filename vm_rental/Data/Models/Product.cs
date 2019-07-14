using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            ClientDiscount = new HashSet<ClientDiscount>();
            MachineResource = new HashSet<MachineResource>();
            OrdersResources = new HashSet<OrdersResources>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductMeasureUnit { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCode { get; set; }
        public byte IsAvailable { get; set; }
        public int ProductTypeProductTypeId1 { get; set; }
        public int ProductSupplierSupplierId { get; set; }

        public virtual ProductSupplier ProductSupplierSupplier { get; set; }
        public virtual ProductType ProductTypeProductTypeId1Navigation { get; set; }
        public virtual ICollection<ClientDiscount> ClientDiscount { get; set; }
        public virtual ICollection<MachineResource> MachineResource { get; set; }
        public virtual ICollection<OrdersResources> OrdersResources { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class Product
    {
        public Product()
        {
            ClientDiscount = new HashSet<ClientDiscount>();
            MachineComponentHistory = new HashSet<MachineComponentHistory>();
            OrdersItems = new HashSet<OrdersItems>();
            ProductHistory = new HashSet<ProductHistory>();
        }

        public int ProductId { get; set; }
        public int ComponentTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual MachineComponentType ComponentType { get; set; }
        public virtual ICollection<ClientDiscount> ClientDiscount { get; set; }
        public virtual ICollection<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual ICollection<OrdersItems> OrdersItems { get; set; }
        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
    }
}

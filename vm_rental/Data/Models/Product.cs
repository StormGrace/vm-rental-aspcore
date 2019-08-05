using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            ClientDiscount = new HashSet<ClientDiscount>();
            MachineComponentHistory = new HashSet<MachineComponentHistory>();
            OrderItems = new HashSet<OrderItems>();
            ProductHistory = new HashSet<ProductHistory>();
        }

        public int ProductId { get; set; }
        public int ComponentTypeId { get; set; }

        public virtual ComponentType ComponentType { get; set; }
        public virtual ICollection<ClientDiscount> ClientDiscount { get; set; }
        public virtual ICollection<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
    }
}

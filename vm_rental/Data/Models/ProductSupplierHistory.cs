using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ProductSupplierHistory
    {
        public int SupplierHistoryId { get; set; }
        public int SupplierId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierDescription { get; set; }
        public byte IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ProductSupplier Supplier { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class InvoiceItem
    {
        public int InvoiceItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemAmount { get; set; }
        public string ItemUnit { get; set; }
        public double ItemUnitPrice { get; set; }
        public double ItemDiscount { get; set; }
        public double ItemTotalPrice { get; set; }
        public int MachineInvoiceLogInvoiceId { get; set; }

        public virtual MachineInvoiceLog MachineInvoiceLogInvoice { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class ClientPayment
    {
        public ClientPayment()
        {
            ClientOrder = new HashSet<ClientOrder>();
        }

        public int PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset DateOfPayment { get; set; }
        public byte IsScheduled { get; set; }
        public int MachineInvoiceLogInvoiceId { get; set; }

        public virtual MachineInvoiceLog MachineInvoiceLogInvoice { get; set; }
        public virtual ICollection<ClientOrder> ClientOrder { get; set; }
    }
}

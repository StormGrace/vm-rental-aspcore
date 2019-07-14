using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class MachineInvoiceLog
    {
        public MachineInvoiceLog()
        {
            ClientPayment = new HashSet<ClientPayment>();
            InvoiceItem = new HashSet<InvoiceItem>();
        }

        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverNumber { get; set; }
        public string ReceiverVatNumber { get; set; }
        public string AccountablePerson { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public byte IsBankPayment { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public byte IsPrepaid { get; set; }
        public int MachineMachineId { get; set; }

        public virtual Machine MachineMachine { get; set; }
        public virtual ICollection<ClientPayment> ClientPayment { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItem { get; set; }
    }
}

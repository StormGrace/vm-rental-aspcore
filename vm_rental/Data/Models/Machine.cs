using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class Machine
    {
        public Machine()
        {
            MachineInvoiceLog = new HashSet<MachineInvoiceLog>();
            MachinePanelLog = new HashSet<MachinePanelLog>();
            MachineResource = new HashSet<MachineResource>();
            MachineResourceLog = new HashSet<MachineResourceLog>();
            MachinesUsers = new HashSet<MachinesUsers>();
        }

        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public DateTime IsMachineActive { get; set; }
        public DateTime DateCreated { get; set; }
        public int ClientClientId { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual ICollection<MachineInvoiceLog> MachineInvoiceLog { get; set; }
        public virtual ICollection<MachinePanelLog> MachinePanelLog { get; set; }
        public virtual ICollection<MachineResource> MachineResource { get; set; }
        public virtual ICollection<MachineResourceLog> MachineResourceLog { get; set; }
        public virtual ICollection<MachinesUsers> MachinesUsers { get; set; }
    }
}

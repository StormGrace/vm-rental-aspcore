using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class MachinesUsers
    {
        public int MachinesUsersId { get; set; }
        public int UserId { get; set; }
        public int MachineId { get; set; }

        public virtual Machine Machine { get; set; }
        public virtual User User { get; set; }
    }
}

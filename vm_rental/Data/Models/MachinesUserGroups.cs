using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class MachinesUserGroups
    {
        public int MachineUserGroupId { get; set; }
        public int MachinesUsersMachineUserId { get; set; }
        public int UserGroupUserGroupId { get; set; }

        public virtual MachinesUsers MachinesUsersMachineUser { get; set; }
        public virtual UserGroup UserGroupUserGroup { get; set; }
    }
}

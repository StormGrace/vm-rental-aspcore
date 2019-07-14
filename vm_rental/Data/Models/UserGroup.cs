using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            GroupsPermissions = new HashSet<GroupsPermissions>();
            MachinesUserGroups = new HashSet<MachinesUserGroups>();
        }

        public int UserGroupId { get; set; }
        public string GroupName { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual ICollection<GroupsPermissions> GroupsPermissions { get; set; }
        public virtual ICollection<MachinesUserGroups> MachinesUserGroups { get; set; }
    }
}

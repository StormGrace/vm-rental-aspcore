using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class UserPermission
    {
        public UserPermission()
        {
            GroupsPermissions = new HashSet<GroupsPermissions>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public byte HasPermission { get; set; }

        public virtual ICollection<GroupsPermissions> GroupsPermissions { get; set; }
    }
}

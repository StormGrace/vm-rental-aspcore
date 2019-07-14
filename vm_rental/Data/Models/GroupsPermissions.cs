using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class GroupsPermissions
    {
        public int GroupPermissionId { get; set; }
        public int UserGroupUserGroupId { get; set; }
        public int UserPermissionPermissionId { get; set; }

        public virtual UserGroup UserGroupUserGroup { get; set; }
        public virtual UserPermission UserPermissionPermission { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class RolesPermissions
    {
        public int RolePermissionId { get; set; }
        public int PermissionPermissionId { get; set; }
        public int RoleRoleId { get; set; }

        public virtual Permission PermissionPermission { get; set; }
        public virtual Role RoleRole { get; set; }
    }
}

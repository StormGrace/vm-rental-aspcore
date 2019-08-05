using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class RolesPermissions
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}

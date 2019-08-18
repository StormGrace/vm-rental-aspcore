using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class Permission 
    {
        public Permission()
        {
            RolesPermissions = new HashSet<RolesPermissions>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public byte HasPermission { get; set; }

        public virtual ICollection<RolesPermissions> RolesPermissions { get; set; }
    }
}

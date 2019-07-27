﻿using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class Role
    {
        public Role()
        {
            RolesPermissions = new HashSet<RolesPermissions>();
        }

        public int RoleId { get; set; }
        public int MachinesUsersMachinesUsersId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string RoleName { get; set; }

        public virtual MachinesUsers MachinesUsersMachinesUsers { get; set; }
        public virtual ICollection<RolesPermissions> RolesPermissions { get; set; }
    }
}
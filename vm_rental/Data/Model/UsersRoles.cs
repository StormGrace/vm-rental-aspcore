using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UsersRoles
    {
        public int UsersRolesId { get; set; }
        //public int RoleId { get; set; }
        //public int UserId { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual User User { get; set; }
    }
}

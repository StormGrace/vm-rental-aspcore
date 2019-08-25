using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UserRoleClaim
    {
        //public int Id { get; set; }
        //public int RoleId { get; set; }
        //public string ClaimType { get; set; }
        //public string ClaimValue { get; set; }

        public virtual UserRole Role { get; set; }
    }
}

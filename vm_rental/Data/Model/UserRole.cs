using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRoleClaim = new HashSet<UserRoleClaim>();
            UsersRoles = new HashSet<UsersRoles>();
        }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string NormalizedName { get; set; }
        //public string ConcurrencyStamp { get; set; }

        public virtual ICollection<UserRoleClaim> UserRoleClaim { get; set; }
        public virtual ICollection<UsersRoles> UsersRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UserLogin
    {
        public int UserLoginId { get; set; }
        //public int UserId { get; set; }
        //public string LoginProvider { get; set; }
        //public string ProviderKey { get; set; }
        //public string ProviderDisplayName { get; set; }

        public virtual User User { get; set; }
    }
}

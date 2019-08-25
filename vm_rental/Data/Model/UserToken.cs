using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UserToken
    {
        public int UserTokenId { get; set; }
        //public int UserId { get; set; }
        //public string LoginProvider { get; set; }
        //public string Name { get; set; }
        //public string Value { get; set; }

        public virtual User User { get; set; }
    }
}

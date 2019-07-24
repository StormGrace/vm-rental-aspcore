using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UserHistory
    {
        public int UserHistoryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int Version { get; set; }
        public byte IsActive { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PwdHash { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual User CreatedByNavigation { get; set; }
    }
}

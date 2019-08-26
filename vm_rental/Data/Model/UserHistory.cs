using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class UserHistory
    {
        public int UserHistoryId { get; set; }
        public int CreatedBy { get; set; }
        public int Version { get; set; }
        public string Changes { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual User CreatedByNavigation { get; set; }
    }
}

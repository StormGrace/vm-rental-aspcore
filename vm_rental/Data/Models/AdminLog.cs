using System;
using System.Collections.Generic;

namespace vm_rental.Data.Models
{
    public partial class AdminLog
    {
        public int LogId { get; set; }
        public DateTimeOffset TimeLogged { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Event { get; set; }
        public string TableName { get; set; }
        public int RecordId { get; set; }
        public string RecordName { get; set; }
        public string RecordValueOld { get; set; }
        public string RecordValueNew { get; set; }
    }
}

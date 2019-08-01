using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_rental.Data.Model
{
    public partial class ClientHistory
    {
        private byte _version = 1;
        private DateTimeOffset _dateCreated;
        private string _firmName;
        private string _firmOwner;
        private string _firmEmail;
        private string _firmPhone;
        private string _firmRegNumber;
        private string _firmVatNumber;
        private string _firmFax;
        private string _state;
        private string _city;
        private string _address;
        private byte _isVatTaxed = 1;
        private byte _isActive = 1;
        
        public ClientHistory()
        {

        }
        public ClientHistory(string firmName, string firmOwner, string firmEmail, string firmPhone,
                            string firmRegNumber,
                            string state, string city, string address) 
    
            : this(firmName, firmOwner, firmEmail, firmPhone, firmRegNumber, null, state, city, address)
        {}
        
        public ClientHistory(string firmName, string firmOwner, string firmEmail, string firmPhone, 
                             string firmRegNumber, string firmVatNumber, 
                             string state, string city, string address)
        {
            _firmName = firmName;
            _firmOwner = firmOwner;
            _firmEmail = firmEmail;
            _firmPhone = firmPhone;
            _firmRegNumber = firmRegNumber;
            _firmVatNumber = firmVatNumber;
            _state = state;
            _city = city;
            _address = address;
            _dateCreated = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientHistoryId { get; set; }
        public int ClientClientId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get { return _dateCreated; } set { _dateCreated = value; } }
        public byte Version { get { return _version; } set { _version = value; } }
        public byte IsActive { get { return _isActive; } set { _isActive = value; } }
        public string FirmName { get { return _firmName; } set { _firmName = value; } }
        public string City { get { return _city; } set { _city = value; } }
        public string FirmNumber { get { return _firmRegNumber; } set { _firmRegNumber = value; } }
        public string VatNumber { get { return _firmVatNumber; } set { _firmVatNumber = value; } }
        public string WithoutVarReason { get; set; }
        public byte IsVatTaxed { get { return _isVatTaxed; } set { _isVatTaxed = value; } }
        public string Address { get { return _address; } set { _address = value; } }
        public string AccPerson { get { return _firmOwner; } set { _firmOwner = value; } }
        public string OrgPhone { get { return _firmPhone; } set { _firmPhone = value; } }
        public string OrgEmail { get { return _firmEmail; } set { _firmEmail = value; } }
        public string OrgFax { get; set; }

        public virtual Client ClientClient { get; set; }
        public virtual User CreatedByNavigation { get; set; }


    }
}

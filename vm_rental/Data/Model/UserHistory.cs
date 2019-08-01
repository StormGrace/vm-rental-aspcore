using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Linq;

namespace vm_rental.Data.Model
{
    public partial class UserHistory
    {
        private int _version = 1;
        private DateTimeOffset _dateCreated;
        private string _userName;
        private string _userEmail;
        private byte[] _pwdHash;
        private string _firstName;
        private string _lastName;
        private string _userPhone;
        private byte _isActive = 1;

        public UserHistory(string userName, string email, byte[] pwdHash,
                        string firstName, string lastName, string phone)
        {
            
        }
        public UserHistory(string userName, string email, string pwdHash, 
                           string firstName, string lastName, string phone)
        {
            _userName = userName;
            _userEmail = email;
            _pwdHash = HashPassword(pwdHash);
            _firstName = firstName;
            _lastName = lastName;
            _userPhone = phone;
            _dateCreated = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserHistoryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset DateCreated { get { return _dateCreated; } set { _dateCreated = value; } }
        public int Version { get { return _version; } set { _version = value; }}
        public byte IsActive { get { return _isActive; } set { _isActive = value; }}
        public string UserName { get { return _userName; } set { _userName = value; }}
        public string FirstName { get { return _firstName; } set { _firstName = value; }}
        public string LastName { get { return _lastName; } set { _lastName = value; }}
        public byte[] PwdHash{ get { return _pwdHash; } set { _pwdHash = value; }}
        public string Email { get { return _userEmail; } set { _userEmail = value; }}
        public string Phone { get { return _userPhone; } set { _userPhone = value; }}
        public string Fax { get; set; }
        public virtual User CreatedByNavigation { get; set; }

        //https://crypto.stackexchange.com/a/43473
        //https://security.stackexchange.com/a/17435
        //https://stackoverflow.com/a/16507766
        private byte[] HashPassword(string passwordStr)
        {
            byte[] password = ASCIIEncoding.ASCII.GetBytes(passwordStr);

            var argon2 = new Argon2id(password);

            byte[] salt = CreateSalt();

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 16; //4 Cores
            argon2.MemorySize = 1024 * 1024; //1 GigaByte
            argon2.Iterations = 1;

            var hash = argon2.GetBytes(16);

            IEnumerable<byte> saltedHash = hash.Concat(salt);

            return saltedHash.ToArray();
        }
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}

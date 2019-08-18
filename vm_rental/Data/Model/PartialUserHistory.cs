using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

//This Class is meant to extend the functionality of it's referenced Entity Class, by protecting it from the EF Generator.
//Add the new functionality here.
namespace vm_rental.Data.Model
{
    public interface IUserHistoryAnnotations
    {
        //Auto-Increment the ID Field on Inserts.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int UserHistoryId { get; set; }
    }
    public partial class UserHistory : IUserHistoryAnnotations
    {
        public UserHistory() { }

        public UserHistory(string username, string userEmail, string userPassword, string firstName, string lastName, string userPhone)
        {
            Username = username;
            UserEmail = userEmail;
            PwdHash = HashPassword(userPassword);
            FirstName = firstName;
            LastName = lastName;
            UserPhone = userPhone;
            Version = 1;
            IsActive = 1;
            DateCreated = DateTime.UtcNow;
        }

        private byte[] HashPassword(string passwordStr)
        {
            byte[] password = ASCIIEncoding.ASCII.GetBytes(passwordStr);

            Argon2id argon2 = new Argon2id(password);

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
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            byte[] buffer = new byte[16];

            rng.GetBytes(buffer);

            return buffer;
        }
    }
}

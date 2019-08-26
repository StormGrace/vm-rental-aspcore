using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Models.Utility.Encrypt
{
  public interface IHasher<T1, T2> where T1 : IHash where T2 : IHashOptions
  {
     string Hash(string providedString, T2 hashOptions);
     T1 SaltedHash(string providedString, string providedSalt, T2 hashOptions);
     bool Verify(string providedString, string providedHash);
     byte[] GenerateSalt(int saltLength);
  }
}

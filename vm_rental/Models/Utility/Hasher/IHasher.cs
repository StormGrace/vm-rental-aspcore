using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Models.Utility
{
  interface IHasher
  {
    byte[] Hash(string text, int hashLength, int saltLength);
    byte[] GenerateSalt(int saltLength);
  }
}

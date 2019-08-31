using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Utility.Security.Hashing.Argon
{
  public interface IArgonConfiguration
  {
     int AlgorithmType { get; set; }
     int AlgorithmVersion { get; set; }
     int Iterations { get; set; }
     int MemorySize { get; set; }
     int Threads { get; set; }
     int Lanes { get; set; }
  }

  //Configuration POCO for ArgonHasher.
  public class ArgonConfiguration : IArgonConfiguration
  {
    public int AlgorithmType { get; set; }
    public int AlgorithmVersion { get; set; }
    public int Iterations { get; set; }
    public int MemorySize { get; set; }
    public int Threads { get; set; }
    public int Lanes { get; set; }
  }
}

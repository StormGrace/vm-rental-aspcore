using System;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using vm_rental.Data.Model;
using vm_rental.Utility.Security.Hashing.Argon;

namespace vm_rental.Models.Identity
{
  public class CustomPasswordHasher : IPasswordHasher<User>
  {
    public ArgonHasher ArgonHasher { get; private set; }

    public CustomPasswordHasher(IArgonConfiguration defaultConfig)
    {
      ArgonHasher = new ArgonHasher(
        new Argon2Config()
        {
          Type    = (Argon2Type)Enum.ToObject(typeof(Argon2Type), defaultConfig.AlgorithmType),
          Version = (Argon2Version)Enum.ToObject(typeof(Argon2Version), defaultConfig.AlgorithmVersion),
          TimeCost   = defaultConfig.Iterations,
          MemoryCost = defaultConfig.MemorySize,
          Threads    = defaultConfig.Threads,
          Lanes      = defaultConfig.Lanes
        }
     );
    }

    public string HashPassword(User user, string password)
    {
      return ArgonHasher.Hash(password);
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
      if(hashedPassword == null || providedPassword == null) { throw new ArgumentNullException(); }

      bool isPasswordValid = ArgonHasher.Verify(providedPassword, hashedPassword);
      
      return isPasswordValid ? PasswordVerificationResult.Success: PasswordVerificationResult.Failed;
    }
  }
}

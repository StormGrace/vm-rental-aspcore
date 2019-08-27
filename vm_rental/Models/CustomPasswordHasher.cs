using System;
using Microsoft.AspNetCore.Identity;
using vm_rental.Data.Model;
using vm_rental.Utility.Security.Hashing.Argon2;

namespace vm_rental.Models
{
  public class CustomPasswordHasher : IPasswordHasher<User>
  {
    public string HashPassword(User user, string password)
    {
      ArgonHasher argonHasher = new ArgonHasher();

      return argonHasher.Hash(password, ArgonHashOptions.Defaults);
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
      if(hashedPassword == null || providedPassword == null) { throw new ArgumentNullException(); }

      ArgonHasher argonHasher = new ArgonHasher();

      bool isPasswordValid = argonHasher.Verify(providedPassword, hashedPassword);
       
      if (isPasswordValid)
      {
        return PasswordVerificationResult.Success;
      }
      else
      {
        return PasswordVerificationResult.Failed;
      }
    }
  }
}

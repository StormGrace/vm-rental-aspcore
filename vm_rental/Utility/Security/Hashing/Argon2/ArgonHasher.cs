using System;
using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace vm_rental.Utility.Security.Hashing.Argon2
{
  //The ArgonHasher class, used for Hashing Passwords.
  //Uses the C# Implementation of Argon2:
    //GitHub: https://github.com/Konscious.Security.Cryptography
    //Documentation: https://password-hashing.net/argon2-specs.pdf
  public class ArgonHasher : IHasher<ArgonHash, ArgonHashOptions>
  {
    public ArgonHasher() {}

    private ArgonHash GenerateArgon2idHash(string text, byte[] salt, ArgonHashOptions hashOptions)
    {
      Argon2id argon2 = new Argon2id(Encoding.UTF8.GetBytes(text))
      {
        DegreeOfParallelism = hashOptions.Parallelism,
        MemorySize = hashOptions.MemorySize,
        Iterations = hashOptions.Iterations,
        Salt = salt
      };

      string hash = Encoding.UTF8.GetString(argon2.GetBytes(32));

      argon2.Dispose();

      return new ArgonHash()
      {
        HashOptions = new ArgonHashOptions
        (
          hashOptions.Algorithm,
          hashOptions.Version,
          hashOptions.MemorySize,
          hashOptions.Iterations,
          hashOptions.Parallelism
        ),

        Salt = Encoding.UTF8.GetString(salt),
        Hash = hash
      };
    }

    public string Hash(string providedPassword, ArgonHashOptions providedHashOptions)
    {
      byte[] salt = GenerateSalt(32);

      ArgonHash argonHash = GenerateArgon2idHash(providedPassword, salt, providedHashOptions);

      return argonHash.ToString();
    }

    public string Hash(string providedPassword, byte[] providedSalt, ArgonHashOptions providedHashOptions)
    {
      ArgonHash argonHash = GenerateArgon2idHash(providedPassword, providedSalt, providedHashOptions);

      return argonHash.ToString();
    }

    public bool Verify(string providedPassword, string providedHash)
    {
      ArgonHash dbHash  = new ArgonHash(providedHash);

      string hash = Hash(providedPassword, Encoding.UTF8.GetBytes(dbHash.Salt), dbHash.HashOptions);

      ArgonHash newHash = new ArgonHash(hash);

      if (newHash.Equals(dbHash))
      {
        return true;
      }

      return false;
    }

    public byte[] GenerateSalt(int saltLength)
    {
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

      byte[] buffer = new byte[saltLength];

      rng.GetBytes(buffer);

      rng.Dispose();

      return buffer;
    }
  }
}

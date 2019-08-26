using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace vm_rental.Models.Utility.Encrypt.Argon2
{
  //The ArgonHasher class, used for Hashing Passwords.
  //Uses the C# Implementation of Argon2:
    //GitHub: https://github.com/Konscious.Security.Cryptography
    //Documentation: https://password-hashing.net/argon2-specs.pdf
  public class ArgonHasher : IHasher<ArgonHash, ArgonHashOptions>
  {
    public ArgonHasher() {}

    public ArgonHash SaltedHash(string providedPassword, string providedSalt, ArgonHashOptions providedHashOptions)
    {
      byte[] salt, hash;
      
      string algorithm, version;
      int memorySize, parallelism, iterations;

      algorithm   = providedHashOptions.Algorithm;
      version     = providedHashOptions.Version;
      memorySize  = providedHashOptions.MemorySize;
      parallelism = providedHashOptions.Parallelism;
      iterations  = providedHashOptions.Iterations;

      Argon2id argon2;

      argon2 = new Argon2id(ASCIIEncoding.ASCII.GetBytes(providedPassword));

      salt = ASCIIEncoding.ASCII.GetBytes(providedSalt);

      argon2.DegreeOfParallelism = parallelism; 
      argon2.MemorySize = memorySize; 
      argon2.Iterations = iterations;
      argon2.Salt = salt;

      hash = argon2.GetBytes(32);

      argon2.Dispose();

      ArgonHash argonHash = new ArgonHash()
      {
        HashOptions = new ArgonHashOptions(algorithm, version, memorySize, iterations, parallelism),
        Salt = providedSalt,
        Hash = Encoding.ASCII.GetString(hash)
      };

      return argonHash;
    }

    public string Hash(string providedPassword, ArgonHashOptions providedHashOptions)
    {
      byte[] salt, hash;
      
      string algorithm, version;
      int memorySize, parallelism, iterations;

      algorithm   = providedHashOptions.Algorithm;
      version     = providedHashOptions.Version;
      memorySize  = providedHashOptions.MemorySize;
      parallelism = providedHashOptions.Parallelism;
      iterations  = providedHashOptions.Iterations;

      Argon2id argon2;

      argon2 = new Argon2id(ASCIIEncoding.ASCII.GetBytes(providedPassword));

      salt = GenerateSalt(16);
   
      argon2.DegreeOfParallelism = parallelism; 
      argon2.MemorySize = memorySize; 
      argon2.Iterations = iterations;
      argon2.Salt = salt;

      hash = argon2.GetBytes(32);

      argon2.Dispose();

      ArgonHash argonHash = new ArgonHash()
      {
        HashOptions = new ArgonHashOptions(algorithm, version, memorySize, iterations, parallelism),
        Salt = Encoding.ASCII.GetString(salt),
        Hash = Encoding.ASCII.GetString(hash)
      };

      return argonHash.ToString();
    }

    public bool Verify(string providedPassword, string providedHash)
    {
      ArgonHash dbHash  = new ArgonHash(providedHash);
      ArgonHash hashProvided = SaltedHash(providedPassword, dbHash.Salt, dbHash.HashOptions);

      if (hashProvided.Equals(dbHash))
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

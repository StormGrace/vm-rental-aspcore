using System;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using vm_rental.Utility.Helpers;

namespace vm_rental.Utility.Security.Hashing.Argon
{
  //The ArgonHasher class, used for Hashing Passwords.
  //Uses the C# Implementation of Argon2:
    //GitHub: https://github.com/mheyman/Isopoh.Cryptography.Argon2
    //Documentation: https://password-hashing.net/argon2-specs.pdf
  public class ArgonHasher : IHasher
  {
    public Argon2Config HashingConfig { get; private set; }

    public ArgonHasher(Argon2Config argonConfig) {

      HashingConfig = argonConfig;
    }

    private string GenerateArgon2Hash(Argon2Config hashConfig)
    {
      Argon2 argon2A = new Argon2(hashConfig);

      string hashString;

      using (SecureArray<byte> hashA = argon2A.Hash())
      {
        hashString = hashConfig.EncodeString(hashA.Buffer);
      }

      argon2A.Dispose();

      return Base64Helper.ToBase64(hashString);
    }

    public string Hash(string providedPassword)
    {
      Argon2Config argonConfig = HashingConfig;

      byte[] salt = GenerateSalt(32);
      byte[] password = Encoding.UTF8.GetBytes(providedPassword);

      argonConfig.Salt = salt;
      argonConfig.Password = password;

      return GenerateArgon2Hash(argonConfig); 
    }

    public string Hash(string providedPassword, int memoryCost, int timeCost, int lanes, int threads)
    {
      Argon2Config argonConfig = new Argon2Config()
      {
        MemoryCost = memoryCost,
        TimeCost = timeCost,
        Lanes = lanes,
        Threads = threads
      };

      byte[] salt = GenerateSalt(32);
      byte[] password = Encoding.UTF8.GetBytes(providedPassword);

      argonConfig.Salt = salt;
      argonConfig.Password = password;

      return GenerateArgon2Hash(argonConfig);
    }

    public bool Verify(string providedPassword, string providedHash)
    {
      bool isValid = false;

      byte[] password = Encoding.UTF8.GetBytes(providedPassword);

      Argon2Config configOfPasswordToVerify = new Argon2Config { Password = password, Threads = 3 };

      Argon2 argon2ToVerify = null;

      SecureArray<byte> hashB = null;

      try
      {
        if (configOfPasswordToVerify.DecodeString(Base64Helper.FromBase64(providedHash), out hashB) && hashB != null)
        {
          argon2ToVerify = new Argon2(configOfPasswordToVerify);

          using (var hashToVerify = argon2ToVerify.Hash())
          {
            if (!hashB.Buffer.Where((b, i) => b != hashToVerify[i]).Any())
            {
              isValid = true;
            }
          }
        }
      }
      finally
      {
        hashB?.Dispose();
        argon2ToVerify.Dispose();
      }

      return isValid;
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

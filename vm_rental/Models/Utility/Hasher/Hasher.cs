using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace vm_rental.Models.Utility
{
  public static class Hasher
  {
    public class PasswordHasher : IHasher
    {

      public byte[] Hash(string password, int hashLength, int saltLength)
      {
        byte[] textArray = null;
        IEnumerable<byte> hashResult = null;

        try
        {
          textArray = ASCIIEncoding.ASCII.GetBytes(password);
        }
        catch (Exception)
        {
          Console.WriteLine("Hashing Failed, Text is null.");
        }

        if (textArray != null)
        {
          Argon2id argon2 = new Argon2id(textArray);

          byte[] salt = GenerateSalt(saltLength);
          byte[] hash;

          argon2.Salt = salt;
          argon2.DegreeOfParallelism = 16; //4 Cores
          argon2.MemorySize = 1024 * 1024; //1 GigaByte
          argon2.Iterations = 1;

          hash = argon2.GetBytes(hashLength);

          hashResult = hash.Concat(salt);

          argon2.Dispose();
        }

        return hashResult.ToArray();
      }
      public byte[] GenerateSalt(int saltLength)
      {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        byte[] buffer = new byte[saltLength];

        rng.GetBytes(buffer);
        rng.Dispose();

        return buffer;
      }
      /*public void Dispose(bool v)
      {
        Dispose(v);
        GC.SuppressFinalize(this);
      }
      protected virtual void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (provider != null) provider.Dispose();
        }
      }*/
    }
  }
}

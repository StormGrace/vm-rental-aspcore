using System;
using vm_rental.Utility.Security.Helpers;

namespace vm_rental.Utility.Security.Hashing.Argon2
{
  //The ArgonHash class, used for storing Argon's Hash Parameters.
  //Reference The ArgonHashOptions class, if the parameters are unfamiliar.
  public class ArgonHash : IHash
  {
    public ArgonHash()
    {
      HashOptions = new ArgonHashOptions();
    }

    public ArgonHash(string hash)
    {
      ExtractFromHashString(hash);
    }

    public ArgonHashOptions HashOptions { get; set; }

    public string Salt { get; set; }

    public string Hash{ get; set; }

    public void ExtractFromHashString(string hashString)
    {
      if (string.IsNullOrEmpty(hashString) == false)
      {
        string decodedString = Base64Helper.FromBase64(hashString);

        string[] hashParams = decodedString.Split("$");

        string algorithm = hashParams[0];
        string version   = hashParams[1];
        string saltParam = hashParams[3];
        string hashParam = hashParams[4];

        int[] metricHashParams = Array.ConvertAll(hashParams[2].Split(','), p => int.Parse(p));

        int memorySize  = metricHashParams[0];
        int iterations  = metricHashParams[1];
        int parallelism = metricHashParams[2];

        HashOptions = new ArgonHashOptions()
        {
          Algorithm   = algorithm,
          Version     = version,
          Iterations  = iterations,
          MemorySize  = memorySize,
          Parallelism = parallelism
        };

        Salt = Base64Helper.FromBase64(saltParam);
        Hash = Base64Helper.FromBase64(hashParam);
      }
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(HashOptions.Algorithm, HashOptions.Version, Salt, Hash);
    }

    public override bool Equals(object obj)
    {
      return obj is ArgonHash argonHash && (Salt.Equals(argonHash.Salt) && Hash.Equals(argonHash.Hash));
    }

    //Returns a Base64 of a string in the format => $algorithm$version$m=...,t=...p=...$salt$hash
    public override string ToString()
    {
      return Base64Helper.ToBase64
      (
        $"${HashOptions.Algorithm}$v={HashOptions.Version}" +
        $"$m={HashOptions.MemorySize},t={HashOptions.Iterations},p={HashOptions.Parallelism}" +
        $"${Base64Helper.ToBase64(Salt)}${Base64Helper.ToBase64(Hash)}"
      );
    }
  };
}

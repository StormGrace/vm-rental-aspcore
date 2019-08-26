using System;

namespace vm_rental.Models.Utility.Encrypt.Argon2
{
  //The ArgonHash class, used for storing Argon's Hash Parameters.
  //Reference The ArgonHashOptions class, if the parameters are unfamiliar.
  public class ArgonHash : IHash
  {
    private ArgonHashOptions hashOptions;

    public ArgonHash()
    {
      hashOptions = new ArgonHashOptions();
    }

    public ArgonHash(string hash)
    {
      ExtractFromHashString(hash);
    }

    public string Salt { get; set; }
    public string Hash { get; set; }

    public ArgonHashOptions HashOptions
    {
      get
      {
        return hashOptions;
      }
      set
      {
        hashOptions = value;
      }
    }

    public void ExtractFromHashString(string hashString)
    {
      if (string.IsNullOrEmpty(hashString) == false)
      {
        string[] hashParams = hashString.Split("$");

        string algorithm = hashParams[0];
        string version   = hashParams[1];
        string salt      = hashParams[3];
        string hash      = hashParams[4];

        int[] metricHashParams = Array.ConvertAll(hashParams[2].Split(','), p => int.Parse(p));

        int memorySize  = metricHashParams[0];
        int iterations  = metricHashParams[1];
        int parallelism = metricHashParams[2];

        Salt = salt;
        Hash = hash;

        hashOptions = new ArgonHashOptions()
        {
          Algorithm = algorithm,
          Version = version,
          Iterations = iterations,
          MemorySize = memorySize,
          Parallelism = parallelism
        };
      }
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Salt, Hash);
    }

    public override bool Equals(object obj)
    {
      return obj is ArgonHash argonHash &&
             hashOptions.Algorithm.Equals(argonHash.hashOptions.Algorithm) &&
             hashOptions.Version.Equals(argonHash.hashOptions.Version) &&
             hashOptions.MemorySize.Equals(argonHash.hashOptions.MemorySize) &&
             hashOptions.Iterations.Equals(argonHash.hashOptions.Iterations) &&
             hashOptions.Parallelism.Equals(argonHash.hashOptions.Parallelism) &&
             Salt.Equals(argonHash.Salt) && Hash.Equals(argonHash.Hash);
    }

    public override string ToString()
    {
      return $"${hashOptions.Algorithm}$v={hashOptions.Version}" +
             $"$m={hashOptions.MemorySize},t={hashOptions.Iterations},p={hashOptions.Parallelism}" +
             $"${Salt}${Hash}";
    }
  };
}

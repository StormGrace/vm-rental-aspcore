

namespace vm_rental.Utility.Security.Hashing
{
  public interface IHasher<T1, T2> where T1 : IHash where T2 : IHashOptions
  {
     string Hash(string providedString, byte[] providedSalt, T2 hashOptions);
     string Hash(string providedString, T2 hashOptions);
     bool Verify(string providedString, string providedHash);
     byte[] GenerateSalt(int saltLength);
  }
}

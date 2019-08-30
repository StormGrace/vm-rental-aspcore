

namespace vm_rental.Utility.Security.Hashing
{
  public interface IHasher
  {
     string Hash(string providedString);
     bool Verify(string providedString, string providedHash);
     byte[] GenerateSalt(int saltLength);
  }
}

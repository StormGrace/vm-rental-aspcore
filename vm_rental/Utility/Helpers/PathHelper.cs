using System.IO;

namespace vm_rental.Utility.Helpers
{
  public static class PathHelper
  {
    public static string FromRoot(string path)
    {
      return (Directory.GetCurrentDirectory() + path.Trim());
    }
  }
}

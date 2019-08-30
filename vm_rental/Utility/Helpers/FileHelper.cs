using System.IO;
using Newtonsoft.Json;

namespace vm_rental.Utility.Helpers
{
  public static class FileHelper
  {

    public static void ReadFromJsonDeserialize<T>(string filePath, ref T jsonReceiver)
    {
      using (StreamReader reader = new StreamReader(filePath))
      {
        string json = reader.ReadToEnd();

        jsonReceiver = JsonConvert.DeserializeObject<T>(json);
      }
    }
  }
}

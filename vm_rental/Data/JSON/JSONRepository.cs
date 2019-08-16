using System;
using System.IO;
using Newtonsoft.Json;


namespace vm_rental.Data.JSON
{
  public static class JSONRepository
  {
    public static Countries countries;
    public static ReservedWords reservedWords;

    static readonly string countriesPath     = (Directory.GetCurrentDirectory() + "\\wwwroot\\data\\countries.json");
    static readonly string reservedWordsPath = (Directory.GetCurrentDirectory() + "\\wwwroot\\data\\reservedwords.json");

    public static void Initialize()
    {
      ReadFromJson(countriesPath, ref countries);
      ReadFromJson(reservedWordsPath, ref reservedWords);
    }

    private static void ReadFromJson<T>(string filePath, ref T jsonReceiver)
    {
      try
      {
        using (StreamReader reader = new StreamReader(filePath))
        {
          string json = reader.ReadToEnd();

          jsonReceiver = JsonConvert.DeserializeObject<T>(json);
        }
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("Can't Find JSON File, such file doesn't exist!");
      }
      catch(Exception)
      {
        Console.WriteLine("Error reading JSON File!");
      }
    }
  }
}

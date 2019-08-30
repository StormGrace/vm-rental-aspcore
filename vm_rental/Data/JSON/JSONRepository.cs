using System;
using System.IO;
using Newtonsoft.Json;
using vm_rental.Utility.Helpers;

namespace vm_rental.Data.JSON
{
  public static class JSONRepository
  {
    public static Countries countries;
    public static ReservedWords reservedWords;

    static readonly string countriesPath     = PathHelper.FromRoot("\\wwwroot\\data\\countries.json");
    static readonly string reservedWordsPath = PathHelper.FromRoot("\\wwwroot\\data\\reservedwords.json");

    public static void Initialize()
    {
      FileHelper.ReadFromJsonDeserialize(countriesPath, ref countries);
      FileHelper.ReadFromJsonDeserialize(reservedWordsPath, ref reservedWords);
    }
  }
}

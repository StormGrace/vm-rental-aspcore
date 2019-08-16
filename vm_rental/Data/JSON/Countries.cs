using System.Collections.Generic;
using System.Linq;


namespace vm_rental.Data.JSON
{
  public class Countries
  {
    public List<Country> countries { get; set; }

    public bool CountryExists(string countryName)
    {                
      return countries.Exists(c => c.Name.Equals(countryName.Trim()));
    }
  }
  public class Country
  {
    private string name;
    private string code2;
    private string country_code;
    private string lang;
    private string lang_native;

    public string Name { get => name; set => name = value; }
    public string Code2 { get => code2; set => code2 = value; }
    public string Country_code { get => country_code; set => country_code = value; }
    public string Lang { get => lang; set => lang = value; }
    public string Lang_native { get => lang_native; set => lang_native = value; }
  }
}

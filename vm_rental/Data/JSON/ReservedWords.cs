using System.Linq;

namespace vm_rental.Data.JSON
{
  public class ReservedWords
  {
    public string[] reservedwords { get; set; }

    public bool WordReserved(string word)
    {
      return reservedwords.Contains(word);
    }
  }
  public class ReservedWord
  {
    public string word;
  }
}

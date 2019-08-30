using System;
using System.Text;


namespace vm_rental.Utility.Helpers
{
  //Helper Class, used for Base64 Encoding and Decoding.
  public static class Base64Helper
  {
    //Encode text in Base64 Format.
    public static string ToBase64(string text)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }
    //Decode text from Base64 Format.
    public static string FromBase64(string text)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(text));
    }
  }
}

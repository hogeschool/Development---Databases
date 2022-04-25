using System.Security.Cryptography;
using System;

namespace Utils
{
  public static class PasswordGenerator
  {
    static public string RandomString 
    {
      get 
      {
        byte[] salt = new byte[512 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
          rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
      }
    }
    public static string Generate(int length)
    {
      char[] symbols = new char[length];
      for (int i = 0; i < length; i++)
      {
        symbols[i] = (char)RandomNumberGenerator.GetInt32(33, 123);
      }
      return new String(symbols);
    }
  }
}

using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Utils
{
  public class PasswordAndSalt 
  {
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
  }
  public static class PasswordHasher
  {
    static public bool CheckHash(string passwordToTest, PasswordAndSalt password) 
    {
      string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: passwordToTest,
          salt: Convert.FromBase64String(password.PasswordSalt),
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));

      return hashed == password.PasswordHash;
    } 

    static public PasswordAndSalt Hash(string password) 
    {
      byte[] salt = new byte[128 / 8];
      using (var rng = RandomNumberGenerator.Create())
      {
          rng.GetBytes(salt);
      }

      // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
      string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));
      return new PasswordAndSalt() { PasswordHash = hashed, PasswordSalt = Convert.ToBase64String(salt) };
    }
  }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forum.Controllers.Users
{
  public class RegistrationData
  {
    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [Required]
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
  }

  public class LoginData
  {
    [Required]
    [JsonPropertyName("emailOrUsername")]
    public string EmailOrUsername { get; set; }
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
  }

  public class ChangePasswordData
  {
    [Required]
    [JsonPropertyName("oldPassword")]
    public string OldPassword { get; set; }

    [Required]
    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set; }
  }
}
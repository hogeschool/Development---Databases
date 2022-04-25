namespace Forum.Services.Data
{
  public interface IRegistrationResponse {  }
  public class SuccessfulRegistration : IRegistrationResponse
  {
    public string Password { get; set; }

    public SuccessfulRegistration(string password)
    {
      Password = password;
    }
  }

  public class UserExisting : IRegistrationResponse {  }

  public enum LoginResponse { SuccessfulLogin, Unauthorized }
  public enum ChangePasswordResponse { Suceeded, Failed }
}
using System.Collections.Generic;
using Forum.Models.Content;

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

  public enum LoginResponse { SuccessfulLogin, Unauthorized, Banned }
  public enum ChangeProfileResponse { Succeeded, Failed }

  public enum AddFriendResponse { Succeeded, FriendExists, NotFound, BadRequest }

  public enum CreateContentResponse { Succeeded, ContentExists }

  public class UserWithTopics
  {
    public string UserName { get; set; }
    public Topic Topic { get; set; }
  }
}
using Microsoft.AspNetCore.Mvc;
using Forum.Services.Users;
using System.Text.Json.Serialization;
using Forum.Services.Data;
using Forum.Services.Authentication;

namespace Forum.Controllers.Users
{

  [Route("user")]
  public class UserManagementController : Controller
  {
    private readonly UserService UserService;
    private readonly AuthenticationHelpers Authentication;

    public UserManagementController(UserService userService, AuthenticationHelpers authentication)
    {
      UserService = userService;
      Authentication = authentication;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginData loginData)
    {
      if (!ModelState.IsValid)
      {
        return StatusCode(400);
      }
      var loginResult = UserService.Login(loginData.EmailOrUsername, loginData.Password, HttpContext);
      switch (loginResult)
      {
        case LoginResponse.Unauthorized:
          return Unauthorized();
        case LoginResponse.SuccessfulLogin:
          return Ok();
        default:
          return StatusCode(500);
      }
    }

    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordData changePasswordData)
    {
      if (!ModelState.IsValid)
      {
        return StatusCode(400);
      }
      var sessionToken = HttpContext.Request.Cookies["UserLogin"];
      return Authentication.AuthenticateAPIOperation<IActionResult>(
        sessionToken,
        () => Unauthorized(),
        (session, userId) =>
        {
          var changePasswordResult = UserService.ChangePassword(userId, changePasswordData.OldPassword, changePasswordData.NewPassword);
          switch (changePasswordResult)
          {
            case ChangePasswordResponse.Failed:
              return Unauthorized();
            case ChangePasswordResponse.Suceeded:
              return Ok();
            default:
              return StatusCode(500);
          }
        }
      );
    }

    

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegistrationData registrationData)
    {
      if (!ModelState.IsValid)
      {
        return StatusCode(400);
      }
      var registrationResult = UserService.Register(registrationData.Email, registrationData.UserName);
      switch (registrationResult)
      {
        case UserExisting:
          return StatusCode(400, new { Reason = "user existing" });
        case SuccessfulRegistration registration:
          var password = registration.Password;
          return Ok(
            new 
            {
              Password = password
            }
          );    
        default:
          return StatusCode(500);
      }
    }
  }
}
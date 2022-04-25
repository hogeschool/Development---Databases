using System;
using System.Linq;
using Forum.Models;
using Forum.Models.Authentication;
using Forum.Models.Users;
using Forum.Services.Authentication;
using Forum.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace Forum.Services.Users
{
  public class UserService
  {
    public ForumContext Context { get; set; }
    public UserService(ForumContext context)
    {
      Context = context;
    }

    public ChangePasswordResponse ChangePassword(int userId, string oldPassword, string newPassword)
    {
      var user =
        Context.User.FirstOrDefault(user => user.Id == userId);

      if (user == null)
      {
        return ChangePasswordResponse.Failed;
      }

      if (!PasswordHasher.CheckHash(oldPassword, new PasswordAndSalt {
        PasswordHash = user.PasswordHash,
        PasswordSalt = user.PasswordSalt
      }))
      {
        return ChangePasswordResponse.Failed;
      }

      var newHashedPassword = PasswordHasher.Hash(newPassword);
      user.PasswordHash = newHashedPassword.PasswordHash;
      user.PasswordSalt = newHashedPassword.PasswordSalt;

      var userSessions =
        (from currentUser in Context.User
        join session in Context.Session on currentUser.Id equals session.UserId
        where currentUser.Id == userId
        select session
        );

      Context.Session.RemoveRange(userSessions);
      Context.SaveChanges();
      return ChangePasswordResponse.Suceeded;
      
    }

    public LoginResponse Login(string emailOrUsername, string password, HttpContext httpContext)
    {
      var user = Context.User.FirstOrDefault(user => user.Email == emailOrUsername || user.UserName == emailOrUsername);
      if (user == null)
      {
        return LoginResponse.Unauthorized;
      }
      if (!PasswordHasher.CheckHash(password, new PasswordAndSalt {
        PasswordHash = user.PasswordHash,
        PasswordSalt = user.PasswordSalt
      }))
      {
        return LoginResponse.Unauthorized;
      }
      
      var sessionToken = PasswordGenerator.RandomString;
      httpContext.Response.Cookies.Append("UserLogin", sessionToken,
        new Microsoft.AspNetCore.Http.CookieOptions()
        {
          Expires = DateTimeOffset.Now.AddMonths(3)
        });
      var session = new Session
      {
        LastLoginAttempt = DateTime.UtcNow,
        IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
        SessionToken = sessionToken,
        User = user
      };
      Context.Session.Add(session);
      Context.SaveChanges();
      return LoginResponse.SuccessfulLogin;
    }

    public IRegistrationResponse Register(string email, string userName)
    {
      var existingUser =
        Context.User.FirstOrDefault(user => user.Email == email || user.UserName == userName);
      if (existingUser != null)
      {
        return new UserExisting();
      }
      string randomPassword = PasswordGenerator.Generate(20);
      var hashedPassword = PasswordHasher.Hash(randomPassword);
      var user = new User
      {
        UserName = userName,
        Email = email,
        PasswordHash = hashedPassword.PasswordHash,
        PasswordSalt = hashedPassword.PasswordSalt
      };
      Context.User.Add(user);
      Context.SaveChanges();
      return new SuccessfulRegistration(randomPassword);
    }


  }
}
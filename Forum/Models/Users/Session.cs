using System;
using Forum.Models.Users;

namespace Forum.Models.Authentication
{
  public class Session : EntityBase
  {
    public string SessionToken { get; set; }
    public DateTime LastLoginAttempt { get; set; }
    public string IpAddress { get; set; }
    public int UserId { get; set; }
    public AuthenticatedUser User { get; set; }
  }
}
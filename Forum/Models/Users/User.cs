

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models.Users
{
  public abstract class AuthenticatedUser : EntityBase
  {
    [Column(TypeName = "varchar(50)")]
    public string UserName { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; }

    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
  }

  public class User : AuthenticatedUser
  {
    public bool Banned { get; set; }
    public IEnumerable<User_Friend> Friends { get; set; }
    public IEnumerable<User_Friend> Befriended { get; set; }
  }

  public class Admin : AuthenticatedUser
  {

  }

  public class User_Friend
  {
    public User User { get; set; }
    public User Friend { get; set; }
    public int UserId { get; set; }
    public int FriendId { get; set; }
  }
}
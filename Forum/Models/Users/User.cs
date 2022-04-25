

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
  }

  public class Admin : AuthenticatedUser
  {

  }
}
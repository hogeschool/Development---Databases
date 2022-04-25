using Microsoft.EntityFrameworkCore;
using Forum.Models.Users;
using Forum.Models.Authentication;

namespace Forum.Models
{
  public class ForumContext : DbContext
  {
    public DbSet<User> User { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Session> Session { get; set; }
    public ForumContext(DbContextOptions<ForumContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AuthenticatedUser>(
        user => user.HasIndex(u => u.Email).IsUnique()
      );
      builder.Entity<AuthenticatedUser>(
        user => user.HasIndex(u => u.UserName).IsUnique()
      );
    }
  }
}



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
    public DbSet<User_Friend> User_Friend { get; set; }
    public ForumContext(DbContextOptions<ForumContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AuthenticatedUser>(
        user => user.HasIndex(u => u.Email).IsUnique()
      );
      builder.Entity<AuthenticatedUser>(
        user => user.HasIndex(u => u.UserName).IsUnique()
      );

      builder
        .Entity<User_Friend>()
        .HasKey(u_f => new { u_f.UserId, u_f.FriendId });

      builder.Entity<User_Friend>()
        .HasOne(u_f => u_f.User)
        .WithMany(u => u.Befriended)
        .HasForeignKey(u_f => u_f.UserId);

      builder.Entity<User_Friend>()
        .HasOne(u_f => u_f.Friend)
        .WithMany(u => u.Friends)
        .HasForeignKey(u_f => u_f.FriendId);

    }
  }
}



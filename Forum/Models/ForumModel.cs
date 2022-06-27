using Microsoft.EntityFrameworkCore;
using Forum.Models.Users;
using Forum.Models.Authentication;
using Forum.Models.Content;

namespace Forum.Models
{
  public class ForumContext : DbContext
  {
    public DbSet<User> User { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<User_Friend> User_Friend { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Topic> Topic { get; set; }
    public DbSet<Section> Section { get; set; }
    public ForumContext(DbContextOptions<ForumContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AuthenticatedUser>(
        user => user.HasIndex(u => u.Email).IsUnique()
      );
      builder.Entity<AuthenticatedUser>(
        user => user.HasIndex(u => u.UserName).IsUnique()
      );
      builder.Entity<News>(
        news => news.HasIndex(n => n.Title).IsUnique()
      );
      builder.Entity<Topic>(
        topic => topic.HasIndex(t => t.Title).IsUnique()
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



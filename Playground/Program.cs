﻿using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;

var r = new Random();
using (var myData = new ProductsDb()) {
  var sw = System.Diagnostics.Stopwatch.StartNew();
  
  IEnumerable<int> ints() {
    var n = 0;
    while(true) yield return n++;
  }

  var myInts = 
    (
      from i in ints()
      where i % 2 == 0
      select i
    ).Take(100).ToList();


  var now = DateTime.Now.ToUniversalTime();
  var myQuery1 = (
    from u in myData.Users
    join uig in myData.UsersInGroup on u.Id equals uig.UserId
    join g in myData.Groups on uig.GroupId equals g.Id
    let name = u.Name
    let surname = u.Surname
    let fullname = name + " " + surname
    let fullGroup = g.Name + " " + g.Description.Substring(0, 10) + "..."
    orderby fullname ascending
    select Tuple.Create(fullname, fullGroup)
  );

  // show the translation into lambda syntax, which is more complete than the pretty LINQ-style syntax
  // show how to do joins in the lambda syntax
  // aggregate and group by
  // advanced modeling - inheritance and owned by
  // more operators like Any
  // subqueries
  // indices for performance optimization

  foreach (var row in myQuery1)
  {
    Console.WriteLine($"{row.Item1}, {row.Item2}");
  }

  // var groups = myData.Groups
  //   .Take(100)
  //   .Include(u => u.UserInGroups)
  //   .ThenInclude(ug => ug.User)
  //   .ToList();

  // foreach (var group in groups)
  // {
  //   Console.WriteLine($"Group {group.Name} {group.Description} contains users {String.Join(", ", group.UserInGroups.Select(ug => ug.User.Name + " " + ug.User.Surname))}");
  // }

  // var u1 = new User(Guid.NewGuid(), "User", "1", DateTime.Now.ToUniversalTime());
  // var u2 = new User(Guid.NewGuid(), "User", "2", DateTime.Now.ToUniversalTime());
  // var u3 = new User(Guid.NewGuid(), "User", "3", DateTime.Now.ToUniversalTime());
  // var u4 = new User(Guid.NewGuid(), "User", "4", DateTime.Now.ToUniversalTime());
  // myData.Users.AddRange(u1, u2, u3, u4);

  // var g1 = new Group(Guid.NewGuid(), "Users", "1 and 2");
  // var g2 = new Group(Guid.NewGuid(), "Users", "2, 3, and 4");
  // myData.Groups.AddRange(g1, g2);

  // myData.UsersInGroup.Add(new UserInGroup(Guid.NewGuid(), u1.Id, g1.Id));
  // myData.UsersInGroup.Add(new UserInGroup(Guid.NewGuid(), u2.Id, g1.Id));

  // myData.UsersInGroup.Add(new UserInGroup(Guid.NewGuid(), u2.Id, g2.Id));
  // myData.UsersInGroup.Add(new UserInGroup(Guid.NewGuid(), u3.Id, g2.Id));
  // myData.UsersInGroup.Add(new UserInGroup(Guid.NewGuid(), u4.Id, g2.Id));
  await myData.SaveChangesAsync();

  Console.WriteLine(sw.ElapsedMilliseconds);
}

namespace Models 
{
  public class ProductsDb : DbContext
  {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<UserInGroup> UsersInGroup { get; set; } = null!;    

    public ProductsDb()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
      optionsBuilder
        .UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=;Database=academy-products-console;Maximum Pool Size=200")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserInGroup>()
        .HasOne(ug => ug.User)
        .WithMany(u => u.UserInGroups)
        .HasForeignKey(ug => ug.UserId);

      modelBuilder.Entity<UserInGroup>()
        .HasOne(ug => ug.Group)
        .WithMany(g => g.UserInGroups)
        .HasForeignKey(ug => ug.GroupId);

      // a user cannot be in more than one group
      // it's a 1-N relation from user to group
      // modelBuilder.Entity<UserInGroup>()
      //   .HasIndex(u => u.UserId)
      //   .IsUnique();

      // modelBuilder.Entity<UserInGroup>()
      //   .HasIndex(u => u.GroupId)
      //   .IsUnique();
    }
  }

  public record User(Guid Id, string Name, string Surname, DateTime Birthday) {
    public List<UserInGroup> UserInGroups { get;set;} = null!;
  };
  public record Group(Guid Id, string Name, string Description) {
    public List<UserInGroup> UserInGroups { get;set;} = null!;
  }
  public record UserInGroup(Guid Id, Guid UserId, Guid GroupId) {
    public User User { get;set;} = null!;
    public Group Group { get;set;} = null!;
  };
}

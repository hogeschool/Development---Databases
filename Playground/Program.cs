using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;

var r = new Random();
using (var myData = new ProductsDb()) {
  var sw = System.Diagnostics.Stopwatch.StartNew();

  // var myProducts = Enumerable.Range(0, 100).Select(i => 
  //   r.NextDouble() > 0.5 ? 
  //     new PerishableProduct(Guid.NewGuid(), $"Product {i+1}", (decimal)(r.NextDouble() * 90 + 10), DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(r.Next(5,100)))) as Product
  //   : new NonPerishableProduct(Guid.NewGuid(), $"Product {i+1}", (decimal)(r.NextDouble() * 90 + 10)) as Product
  // ).ToArray();
  // var myCustomers = Enumerable.Range(0, 100).Select(i => 
  //   new Customer(Guid.NewGuid(), $"John {i+1}", $"Doe {i+1}", new DateOnly(r.Next(1970, 2005), r.Next(1, 11), r.Next(1, 25)))
  // ).ToArray();
  // var myOrders = Enumerable.Range(0, 100).Select(_ => 
  //   new Order(Guid.NewGuid(), myCustomers[r.Next(myCustomers.Length)].Id, myProducts[r.Next(myProducts.Length)].Id, DateTime.UtcNow - TimeSpan.FromDays(r.Next(500)))
  //   { DeliverTo = new StreetAddress($"Street {_}", $"City {_}")}
  // ).ToArray();

  // myData.Products.AddRange(myProducts);
  // myData.Customers.AddRange(myCustomers);
  // myData.Orders.AddRange(myOrders);
  // myData.SaveChanges();

  // myData.Products.Add(new PerishableProduct(Guid.NewGuid(), "Red oranges from Sicily", 10m, DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(20))));
  // myData.Products.Add(new PerishableProduct(Guid.NewGuid(), "Greek Yoghurt from Santorini", 5m, DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(10))));
  // myData.Products.Add(new PerishableProduct(Guid.NewGuid(), "Radioactive Uranium Pellets", 5000000m, DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(365) * 160)));
  // myData.Products.Add(new NonPerishableProduct(Guid.NewGuid(), "Airpods Pro 2nd gen.", 350m));
  // myData.Products.Add(new NonPerishableProduct(Guid.NewGuid(), "BMW X5 e45 Plugin Hybrid", 160000m));
  // myData.SaveChanges();



  var myQuery = myData.Orders.ToArray();

  foreach (var v in myQuery)
  {
    Console.WriteLine(v);
  }

  // show the translation into lambda syntax, which is more complete than the pretty LINQ-style syntax
  // show how to do joins in the lambda syntax
  // more operators like Any
  // subqueries
  // indices for performance optimization


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
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<PerishableProduct> PerishableProducts { get; set; } = null!;
    public DbSet<NonPerishableProduct> NonPerishableProducts { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
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

      modelBuilder.Entity<Order>()
        .HasOne(o => o.Product)
        .WithMany(p => p.Orders)
        .HasForeignKey(o => o.ProductId);

      modelBuilder.Entity<Order>()
        .HasOne(o => o.Customer)
        .WithMany(p => p.Orders)
        .HasForeignKey(o => o.CustomerId);

      modelBuilder.Entity<Product>()
        .HasDiscriminator(p => p.ProductType)
        .HasValue<PerishableProduct>(ProductType.Perishable)
        .HasValue<NonPerishableProduct>(ProductType.NonPerishable);

      modelBuilder.Entity<Order>()
        .OwnsOne(o => o.DeliverTo);

      // modelBuilder.Entity<AsyncProcess<S>>()
      //   .OwnsOne(o => o.Payload);

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

  public enum ProductType { Perishable, NonPerishable }
  public abstract record Product(Guid Id, string Name, decimal Price, ProductType ProductType) {
    public List<Order> Orders { get; set; } = null!;
  }
  public record PerishableProduct(Guid Id, string Name, decimal Price, DateOnly KeepUntil) : Product(Id, Name, Price, ProductType.Perishable) {
  }
  public record NonPerishableProduct(Guid Id, string Name, decimal Price) : Product(Id, Name, Price, ProductType.NonPerishable) {
  }

  public record Customer(Guid Id, string Name, string Surname, DateOnly Birthday) {
    public List<Order> Orders { get; set; } = null!;
  }
  public record Order(Guid Id, Guid CustomerId, Guid ProductId, DateTime When) {
    public Customer Customer { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public StreetAddress DeliverTo { get; set; } = null!;
  }
  public record StreetAddress(string Street, string City);

  public record User(Guid Id, string Name, string Surname, DateTime Birthday) {
    public List<UserInGroup> UserInGroups { get;set;} = null!;
  }
  public record Group(Guid Id, string Name, string Description) {
    public List<UserInGroup> UserInGroups { get;set;} = null!;
  }
  public record UserInGroup(Guid Id, Guid UserId, Guid GroupId) {
    public User User { get;set;} = null!;
    public Group Group { get;set;} = null!;
  }

  public enum Priority { High, Medium, Low }
  public enum Status { Running, Done, StuckOnError }
  public class AsyncProcess<S> {
    public Guid Id { get; set; }
    public S Payload { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime DueAfter { get; set; }
    public string LogMessage { get; set; }
    public int Retries { get; set; }
    public Guid SMId { get; set; }
    public int SequenceNr { get; set; }
  }
}

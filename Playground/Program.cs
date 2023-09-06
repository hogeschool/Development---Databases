using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;


var r = new Random();
using (var myData = new ProductsDb()) {
  var sw = System.Diagnostics.Stopwatch.StartNew();

  // write queries here
  myData.PerishableProducts.Add(new PerishableProduct(Guid.NewGuid(), "First product", 99m, DateOnly.FromDateTime(DateTime.Now)));
  await myData.SaveChangesAsync();

  Console.WriteLine(sw.ElapsedMilliseconds);
}

namespace Models 
{
  public class ProductsDb : DbContext
  {
    public ProductsDb()
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<PerishableProduct> PerishableProducts { get; set; } = null!;
    public DbSet<NonPerishableProduct> NonPerishableProducts { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
      optionsBuilder
        .UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=;Database=academy-products-console;Maximum Pool Size=200")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Order>()
        .HasOne(o => o.Product)
        .WithMany(p => p.Orders)
        .HasForeignKey(o => o.ProductId);
      modelBuilder.Entity<Order>()
        .HasOne(o => o.Customer)
        .WithMany(c => c.Orders)
        .HasForeignKey(p => p.CustomerId);
      modelBuilder.Entity<Order>()
        .HasIndex(_ => _.When);
      modelBuilder.Entity<Product>()
        .HasDiscriminator(b => b.ProductType)
        .HasValue<NonPerishableProduct>(ProductType.NonPerishable)
        .HasValue<PerishableProduct>(ProductType.Perishable);
      modelBuilder.Entity<Order>().OwnsOne(p => p.DeliverTo);
    }
  }

  public enum ProductType { Perishable, NonPerishable }
  public abstract record Product(Guid Id, string Name, decimal Price, ProductType ProductType) {
    public List<Order> Orders { get; set; } = null!;
  }
  public record NonPerishableProduct(Guid Id, string Name, decimal Price) : Product(Id, Name, Price, ProductType.NonPerishable) {
  }
  public record PerishableProduct(Guid Id, string Name, decimal Price, DateOnly KeepUntil) : Product(Id, Name, Price, ProductType.Perishable) {
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
}

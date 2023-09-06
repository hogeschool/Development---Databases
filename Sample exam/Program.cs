using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;


var r = new Random();
// using (var myData = new ProductsDb()) {
  var sw = System.Diagnostics.Stopwatch.StartNew();

  // write queries here

// }
Console.WriteLine(sw.ElapsedMilliseconds);

namespace Models 
{
  public class ProductsDb : DbContext
  {
    public ProductsDb()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
      optionsBuilder
        .UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=;Database=academy-products-console;Maximum Pool Size=200")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
  }

}

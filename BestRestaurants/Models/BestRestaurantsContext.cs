using Microsoft.EntityFrameworkCore;

namespace BestRestaurants.Models
{
  public class BestRestaurantsContext : DbContext
  {
    public DbSet<Type> Types { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<TypeRestaurant> TypeRestaurant { get; set; }

    public BestRestaurantsContext(DbContextOptions options) : base(options){}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}
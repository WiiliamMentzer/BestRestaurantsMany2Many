using System.Collections.Generic;

namespace BestRestaurants.Models
{
  public class Restaurant 
  {

    public Restaurant()
    {
      this.JoinEntities = new HashSet<TypeRestaurant>();
    }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public int Cost { get; set; }
    public string Review { get; set; }
    public virtual ICollection<TypeRestaurant> JoinEntities { get; set; }
  }
}
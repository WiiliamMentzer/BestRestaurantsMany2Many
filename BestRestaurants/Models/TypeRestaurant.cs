namespace BestRestaurants.Models
{
  public class TypeRestaurant
  {
    public int TypeRestaurantId { get; set; }
    public int TypeId { get; set; }
    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual Type Type { get; set; }
  }
}
using System.Collections.Generic;

namespace BestRestaurants.Models
{
  public class Type
  {
    public Type()
    {
      this.JoinEntities = new HashSet<TypeRestaurant>();
    }

    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public virtual ICollection<TypeRestaurant> JoinEntities { get; set; }
  }
}
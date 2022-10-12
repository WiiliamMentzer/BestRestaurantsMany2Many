using System.Collections.Generic; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller 
  { 

    private readonly BestRestaurantsContext _db;

    public RestaurantsController(BestRestaurantsContext db) 
    {
      _db = db; 
    }

    public ActionResult Index()
    {
      List<Restaurant> model = _db.Restaurants.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      return View(); 
    }

    [HttpPost]
    public ActionResult Create(Restaurant restaurant)
    {
      _db.Restaurants.Add(restaurant); 
      _db.SaveChanges(); 
      return RedirectToAction("Index"); 
    }

    public ActionResult Details(int id)
    {
      var thisRestaurant = _db.Restaurants
      .Include(restaurant => restaurant.JoinEntities)
      .ThenInclude(join => join.Type)
      .FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    public ActionResult Edit(int id)
    {
      var thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    [HttpPost]
    public ActionResult Edit(Restaurant restaurant)
    {
      _db.Entry(restaurant).State = EntityState.Modified;
      _db.SaveChanges(); 
      return RedirectToAction("Index"); 
    }

    public ActionResult Delete(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      _db.Restaurants.Remove(thisRestaurant); 
      _db.SaveChanges(); 
      return RedirectToAction("Index"); 
    }

    public ActionResult AddType(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants
        .Include(restaurant => restaurant.JoinEntities)
        .ThenInclude(join => join.Restaurant)
        .FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      return View(thisRestaurant);
    }

    [HttpPost]
    public ActionResult AddType(Restaurant restaurant, int TypeId)
    {
      if (TypeId != 0)
      {
        _db.TypeRestaurant.Add(new TypeRestaurant() {TypeId = TypeId, RestaurantId = restaurant.RestaurantId});
        _db.SaveChanges();
      }
    return RedirectToAction("Index");
    }
  }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BestRestaurants.Models;



namespace BestRestaurants.Controllers
{
  public class TypesController : Controller
  {
    private readonly BestRestaurantsContext _db;

    public TypesController(BestRestaurantsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Types.ToList()); 
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Type type, int RestaurantId)
    {
      _db.Types.Add(type);
      _db.SaveChanges();
      if (RestaurantId != 0)
      {
        _db.TypeRestaurant.Add(new TypeRestaurant() {TypeId = type.TypeId, RestaurantId = RestaurantId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisType = _db.Types
        .Include(type => type.JoinEntities)
        .ThenInclude (join => join.Restaurant)
        .FirstOrDefault(type => type.TypeId == id);
      return View(thisType);
    }

    public ActionResult Edit(int id)
    {
      var thisType = _db.Types.FirstOrDefault(type => type.TypeId == id);
      return View(thisType);
    }

    [HttpPost]
    public ActionResult Edit(Type type)
    {
      _db.Entry(type).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Type thisType = _db.Types.FirstOrDefault(type => type.TypeId == id);
      return View(thisType);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Type thisType = _db.Types.FirstOrDefault(type => type.TypeId == id);
      _db.Types.Remove(thisType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
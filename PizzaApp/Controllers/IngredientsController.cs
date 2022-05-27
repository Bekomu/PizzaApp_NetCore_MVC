using Microsoft.AspNetCore.Mvc;
using PizzaApp.Filters;
using PizzaApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaApp.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly AppDbContext _db;

        public IngredientsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if(TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View(_db.Ingredients.ToList());
        }

        [AdminLogin]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var deleted = _db.Ingredients.FirstOrDefault(x => x.Id == id);
                _db.Remove(deleted);
                _db.SaveChanges();
                TempData["message"] = $"{deleted.Name} successfully deleted";
                return RedirectToAction("Index", "Ingredients");
            }
            return RedirectToAction("Index", "Ingredients");
        }

        [AdminLogin]
        public IActionResult NewIngredient()
        {
            return View();
        }

        [AdminLogin]
        [HttpPost]
        public IActionResult NewIngredient(NewIngredientViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Ingredient newIngredient = new Ingredient()
                {
                    Name = vm.Name
                };
                _db.Ingredients.Add(newIngredient);
                _db.SaveChanges();
                TempData["message"] = $"{newIngredient.Name} successfully saved.";
                return RedirectToAction("Index", "Ingredients");
            }
            return View();
        }
    }
}

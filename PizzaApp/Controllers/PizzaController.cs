using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Filters;
using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PizzaApp.Controllers
{
    public class PizzaController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PizzaController(AppDbContext db, IWebHostEnvironment _env)
        {
            _db = db;
            this._env = _env;
        }

        [AdminLogin]
        public IActionResult EditPizza(int id)
        {
            var pizza = _db.Pizzas.Find(id);

            if(pizza == null) return NotFound();


            var vm = new EditPizzaViewModel();
            vm.Id = id;
            vm.Name = pizza.Name;
            vm.Ingredients = _db.Ingredients.ToList();

            if(pizza.Ingredients != null)
            {
                foreach (var ingredient in pizza.Ingredients)
                {
                    vm.SelectedIngredients.Add(ingredient.Id);
                }
            }
            return View(vm);
        }

        [AdminLogin]
        [HttpPost]
        public IActionResult EditPizza(EditPizzaViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var editedPizza = _db.Pizzas.FirstOrDefault(p => p.Id == vm.Id);
                editedPizza.Name = vm.Name;
                foreach (var ingredientId in vm.SelectedIngredients)
                {
                    editedPizza.Ingredients.Add(_db.Ingredients.Find(ingredientId));
                }

            }
            vm.Ingredients = _db.Ingredients.ToList();

            return View(vm);
        }

        [AdminLogin]
        public IActionResult NewPizza()
        {
            var vm = new CreatePizzaViewModel();
            vm.Ingredients = _db.Ingredients.ToList();
            return View(vm);
        }

        [AdminLogin]
        [HttpPost]
        public IActionResult NewPizza(CreatePizzaViewModel vm)
        {
            vm.Ingredients = _db.Ingredients.ToList();
            if (ModelState.IsValid)
            {
                var newPizza = new Pizza();
                newPizza.Name = vm.Name;
                newPizza.Ingredients = new List<Ingredient>();

                foreach (var ingredientId in vm.SelectedIngredients)
                {
                    newPizza.Ingredients.Add(_db.Ingredients.Find(ingredientId));
                }

                // photo file code area

                string filePath = Path.GetExtension(vm.Photo.FileName);    // gerçek dosya adı path oldu
                string fileName = Guid.NewGuid() + filePath;   // dosyanın kayıt edileceği isim başına guid ekledik
                string savePath = Path.Combine(_env.WebRootPath, "img", fileName);  // kayıt edileceği yolu belirttik.

                using (var stream = System.IO.File.Create(savePath))
                {
                    vm.Photo.CopyTo(stream);
                }

                newPizza.ImagePath = fileName;  // class tarafındaki string prop

                _db.Pizzas.Add(newPizza);
                _db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        [AdminLogin]
        public IActionResult DeleteIndex(int id)
        {
            var pizza = _db.Pizzas.Find(id);
            var vm = new DeletePizzaViewModel();
            vm.Name = pizza.Name;
            vm.Id = pizza.Id;
            return View(vm);
        }

        [AdminLogin]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeletePizza(int id)
        {
            if (id != null)
            {
                var pizza = _db.Pizzas.Find(id);
                _db.Pizzas.Remove(pizza);
                _db.SaveChanges();
                TempData["deleted"] = $"{pizza.Name}";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}

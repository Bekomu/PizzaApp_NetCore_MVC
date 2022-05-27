using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Models;

namespace PizzaApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;

        public LoginController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(Admin administrator)
        {
            if (ModelState.IsValid)
            {
                if (administrator.Password == "sbbo1993")
                {
                    HttpContext.Session.SetString("admin", "girdi");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("admin") == "girdi")
            {
                HttpContext.Session.Remove("admin");
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }
    }
}

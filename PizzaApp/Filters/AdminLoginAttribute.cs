using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PizzaApp.Filters
{
    public class AdminLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("admin") != "girdi")
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }
    }
}

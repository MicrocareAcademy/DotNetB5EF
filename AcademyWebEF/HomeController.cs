using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            //string userName = Request.Cookies["MyUserKey"];

            // string userName = HttpContext.Session.GetString("Username");

            string userName = HttpContext.User.Identity.Name;

            return View("Dashboard",userName);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            //string userName = Request.Cookies["MyUserKey"];

            string userName = HttpContext.Session.GetString("Email");

            return View("Dashboard",userName);
        }
    }
}

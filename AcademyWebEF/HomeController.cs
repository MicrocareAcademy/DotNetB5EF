using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            string userName = Request.Cookies["MyUserKey"];

            return View("Dashboard",userName);
        }
    }
}

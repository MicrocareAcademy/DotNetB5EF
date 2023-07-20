using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}

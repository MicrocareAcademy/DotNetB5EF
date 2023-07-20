using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public RedirectToActionResult SubmitLogin()
        {
            return RedirectToAction("Dashboard", "Home");
        }
    }
}

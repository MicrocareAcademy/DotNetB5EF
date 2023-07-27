using AcademyWebEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademyWebEF
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult SubmitLogin(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                //we will send request to DB to check the user name & password
                // if we have user with user name and password, then user will be redirected to home page
                // else we will show validation message

                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login Failed, please validate your username & password!");

                return View("Login", model);
            }
            
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}

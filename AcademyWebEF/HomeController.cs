using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
            //return View("Dashboard", userName);

            ViewData["username"] = userName;

            ViewBag.UserName = userName;
            ViewBag.Title = "Dashboard";

            return View("Dashboard");


        }

        public ActionResult GetUsers()
        {
            return View();
        }
        public ViewResult Login()
        {
            return View();
        }
        public RedirectToActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        public JsonResult GetUserDetails()
        {
            var userInfo = new
            {
                UserName = "Sajjad Ali Khan",
                MobileNo = "9639632587"
            };

            return Json(userInfo);
        }

        public ContentResult GetUserName()
        {
            return Content("SAJJAD ALI KHAN");
        }

        public EmptyResult EmptyResult()
        {
            return new EmptyResult();
        }
    }
}

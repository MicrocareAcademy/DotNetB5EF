using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

                var dbContext = new AcademyDbContext();

                User userEntity = dbContext.Users
                                     .FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);

                if(userEntity is null)
                {
                    // there is no user with email and password provided
                    ModelState.AddModelError("", "Login Failed, please validate your username & password!");

                    return View("Login", model);
                }

                //User is valid and successful login

                string userId = userEntity.UserId.ToString();
                string userName = userEntity.UserName;
                string userEmail = userEntity.Email;

                var claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.UserData, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email,userEmail)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = false,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    IssuedUtc = DateTimeOffset.Now,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimPrincipal, authProperties);

                //CookieOptions option = new()
                //{
                //    Expires = authProperties.ExpiresUtc
                //};

                //HttpContext.Response.Cookies.Append("LogInUserCookieKey", userId, option);
                

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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }


        // COOKIE STATE MANAGEMENT
        //CookieOptions options = new CookieOptions();
        //options.Expires = DateTime.Now.AddMinutes(1);
        //options.Secure = true;

        //Response.Cookies.Append("MyUserKey", model.Email, options);
    }
}

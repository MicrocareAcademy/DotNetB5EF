using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using AcademyWebEF.Services;
using AcademyWebEF.Interfaces;

namespace AcademyWebEF
{
    public class AccountController : Controller
    {
        private readonly StudentService studentService;
        private readonly IUserService userService;

        public AccountController()
        {
            studentService = new StudentService();
            userService = new UserService();
        }

        [HttpGet]
        public ActionResult Login()
        {

            // Check if the user is already authenticated
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Roles.Admin))
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                // while creating claim, we are keeping userid inside claimtype serial number, its just for demo purpose
                var userId = User.FindFirstValue(ClaimTypes.SerialNumber);

                // if we have an userid then we need to check for role
                // if role is admin then go to dashboard page 
                // if roles is student then we need to get student id from user id and then
                // navigate to student profile page

                if (string.IsNullOrEmpty(userId) == false)
                {
                    var student = studentService.GetStudentByUserId(Convert.ToInt32(userId));

                    if (student != null)
                    {
                        return RedirectToAction("StudentRO", "Student", new { studentId = student.StudentId });
                    }
                }

            }

            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult SubmitLogin
            (LoginModel model)
        {

            if (ModelState.IsValid)
            {
                //we will send request to DB to check the user name & password
                // if we have user with user name and password, then user will be redirected to home page
                // else we will show validation message

                User? userEntity = userService.GetUserByCredentails(model.Email, model.Password);

                if (userEntity is null)
                {
                    // there is no user with email and password provided
                    ModelState.AddModelError("", "Login Failed, please validate your username & password!");

                    return View("Login", model);
                }

                string cliamName = string.Empty;
                Student? student = null;

                if (userEntity.Role == Roles.Admin)
                {
                    cliamName = userEntity.UserName;
                }
                else
                {
                    student = studentService.GetStudentByUserId(userEntity.UserId);

                    cliamName = student.StudentName;

                }

                //User is valid and successful login

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, cliamName),
                    new Claim(ClaimTypes.Name, userEntity.UserName),
                    new Claim(ClaimTypes.Email,userEntity.Email),
                    new Claim(ClaimTypes.Role,userEntity.Role),
                    new Claim(ClaimTypes.SerialNumber,userEntity.UserId.ToString())
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

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);

                if (userEntity.Role == Roles.Admin)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    return RedirectToAction("StudentRO", "Student", new { studentId = student.StudentId });
                }

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

        public IActionResult AccessDenied()
        {
            return View();
        }
        
    }
}

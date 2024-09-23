using BusinessLogic;
using DataAccess;
using E_starta.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace E_starta.Controllers
{
    public class LoginController : Controller
    {
        private LoginLogic _logic;
        private LoginModel _model;
        public LoginController(LoginLogic login)
        {
            _logic = login;
            _model = new LoginModel();
        }

        public IActionResult Index()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var userType = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value ?? "0");
                // Redirect to the appropriate dashboard based on user type
                switch (userType)
                {
                    case 0: return RedirectToAction("Index", "Admin");
                    case 1: return RedirectToAction("Index", "Regular");
                    case 2: return RedirectToAction("Index", "Employee");
                    case 3: return RedirectToAction("Index", "Manager");
                    default: return RedirectToAction("Index", "Login");
                }
            }

            // Display the login page if the user is not authenticated
            if (TempData["ErrorMessage"] != null) _model.result.ErrorMessage = TempData["ErrorMessage"].ToString();
            if (TempData["Message"] != null) _model.result.Message = TempData["Message"].ToString();

            return View("Index", _model);
        }



        [HttpPost]
        public IActionResult Index(string EmailTextBox, string PasswordTextBox)
        {
            ResultValidation result = new ResultValidation();
            result = _logic.ValidUser(EmailTextBox, PasswordTextBox);
            if (result.IsValid)
            {
                var user = _logic.GetUser(EmailTextBox);

                _model.user.Email = user.Email;
                _model.user.Password = user.Password;
                _model.user.Username = user.Username;
                _model.user.UserType = user.UserType;

                result.Message = "Wellcome";
                TempData["Message"] = result.Message;



                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.UserType.ToString())
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };


                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();


                HttpContext.Session.SetInt32("userId", user.Id);
                HttpContext.Session.SetInt32("employeeId", user.Id);

                switch (_model.user.UserType)
                {
                    case 0: return RedirectToAction("Index", "Admin");
                    case 1: return RedirectToAction("Index", "Regular");
                    case 2: return RedirectToAction("Index", "Employee");
                    case 3: return RedirectToAction("Index", "Manager");
                    default: return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction("Index");
            }   
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out from the cookie authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear session data
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}

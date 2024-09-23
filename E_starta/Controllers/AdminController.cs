using BusinessLogic;
using E_starta.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_starta.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminLogic _logic;

        public AdminController(AdminLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new AdminModel
            {
                Users = _logic.GetUsers()
            };

            if (TempData["ErrorMessage"] != null)
            {
                model.result = new ResultValidation
                {
                    ErrorMessage = TempData["ErrorMessage"].ToString()
                };
            }
            if (TempData["Message"] != null)
            {
                model.result = new ResultValidation
                {
                    Message = TempData["Message"].ToString()
                };
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddUser(string Username, string Email, string Password, int UserType)
        {
            var model = new AdminModel
            {
                Email = Email,
                Password = Password,
                Username = Username,
                UserType = UserType
            };

            model.Users = _logic.GetUsers();
            var result = _logic.IsValid(Username, Email, Password);
            if (result.IsValid)
            {
                _logic.AddUser(Username, Email, Password, UserType);
                TempData["Message"] = "Added Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            if (id == null || id <= 0)
            {
                TempData["ErrorMessage"] = "Unexpected Id";
                return RedirectToAction("Index");   
            }
            else
            {
                _logic.DeleteUser(id);
                var model = new AdminModel
                {
                    Users = _logic.GetUsers()
                };
                TempData["Message"] = "Deleted successfully";
                return RedirectToAction("Index");
            }
        }
    }
}

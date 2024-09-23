using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessLogic;


namespace BusinessLogic
{
    public class LoginLogic
    {
        private DbContextController _controller;

        public LoginLogic(DbContextController controller)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }
        public UserData GetUser(string Email)
        {
            return _controller.GetUserData(Email);
        }
        public ResultValidation ValidUser(string Email, string Password)
        {
            var result = new ResultValidation();

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                result.IsValid = false;
                result.ErrorMessage = "Email or Password cannot be null or empty.";
                return result;
            }

            if (_controller.IsValid(Email, Password))
            {
                result.IsValid = true;
                return result;
            }
            result.IsValid = false;
            result.ErrorMessage = "Invalid email or password";
            return result;

        }
    }
}

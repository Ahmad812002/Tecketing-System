using DataAccess;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;


namespace BusinessLogic
{
    public class AdminLogic
    {
        private readonly DbContextController _controller;
        public ResultValidation result;
        public AdminLogic(DbContextController controller)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _controller = new DbContextController(configuration);
        }

        public void AddUser(string Username, string Email, string Password, int UserType)
        {
            _controller.AddUser(Username, Email, Password, UserType);
        }
        public void DeleteUser(int Id)
        {
            _controller.DeleteUser(Id);
        }
        public List<UserData> GetUsers()
        {
            return _controller.GetUsers();
        }


        public ResultValidation IsValid(string Username, string Email, string Password)
        {

            result = new ResultValidation();
            string spectialCharacters = @"[!@#$%^&*()_\-+=\[\]{}|;:'"",.<>/?~`]";
            string UpperOrLower = @"(?=.*[A-Z])";
            string digets = @"(?=.*\d)";

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                result.ErrorMessage = "fill the blockes";
                return result;
            }
            var user = _controller.GetUserData(Email);
            if (user != null)
            {
                result.ErrorMessage = "this user has an account";
                result.IsValid = false;
                return result;
            }
            else if (Password.Length < 8)
            {
                result.ErrorMessage = "Password can't be less than 8 letters";
                result.IsValid = false;
                return result;
            }
            else if (!Regex.IsMatch(Password, spectialCharacters))
            {
                result.ErrorMessage = "Password must contain at least one special character";
                result.IsValid = false;
                return result;
            }
            else if (!Regex.IsMatch(Password, UpperOrLower))
            {
                result.ErrorMessage = "Password must contain at least one upper character";
                result.IsValid = false;
                return result;
            }
            else if (!Regex.IsMatch(Password, digets))
            {
                result.ErrorMessage = "Password must contain at least one diget";
                result.IsValid = false;
                return result;
            }

            else if (!Email.Contains("@") || !Email.Contains(".com"))
            {
                result.ErrorMessage = "Email is incorrect";
                result.IsValid = false;
                return result;
            }
            else if (Email.Length < 5)
            {
                result.ErrorMessage = "Email can't be less than 5 characters";
                result.IsValid = false;
                return result;
            }
            result.IsValid = true;
            return result;
        }
    }
}

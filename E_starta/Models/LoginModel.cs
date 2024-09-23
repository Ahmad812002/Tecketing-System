using BusinessLogic;
using DataAccess;

namespace E_starta.Models
{
    public class LoginModel
    {
        public UserData user { get; set; } = new UserData();
        public ResultValidation result { get; set; } = new ResultValidation();
    }
}

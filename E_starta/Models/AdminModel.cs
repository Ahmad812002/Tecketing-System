using BusinessLogic;
using DataAccess;

namespace E_starta.Models
{
    public class AdminModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }

        public List<UserData> Users { get; set; }
        public ResultValidation result { get; set; }

    }
}

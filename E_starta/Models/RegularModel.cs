using BusinessLogic;
using DataAccess;

namespace E_starta.Models
{
    public class RegularModel
    {
        public TicketData? ticket = new TicketData();
        public List<TicketData>? tickets = new List<TicketData>();
        public UserData? user = new UserData();
        public List<UserData> users = new List<UserData>();
        public ResultValidation result = new ResultValidation();
    }
}

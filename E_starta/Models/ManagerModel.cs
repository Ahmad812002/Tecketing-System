using BusinessLogic;
using DataAccess;
using Microsoft.Identity.Client;

namespace E_starta.Models
{
    public class ManagerModel
    {
        public List<TicketData> tickets { get; set; } = new List<TicketData>(); 
        public List<ManagerData> manager { get; set; } = new List<ManagerData>();
        public UserData user { get; set; } = new UserData();
        public UserData employee { get; set; } = new UserData();
        public TicketData ticket { get; set; } = new TicketData();
        public ResultValidation result { get; set; } = new ResultValidation();
        public List<ManagerData> managers { get; set; } = new List<ManagerData>();


    }
}

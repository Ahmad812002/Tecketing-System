using BusinessLogic;
using DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;

namespace E_starta.Models
{
    public class EmployeeModel
    {
        public List<TicketData> tickets { get; set; } = new List<TicketData>();
        public List<EmployeeData> employees { get; set; } = new List<EmployeeData>();
        public UserData user { get; set; } = new UserData();
        public int numberOfTicketsAssigned { get; set; } = 0;
        public ResultValidation result { get; set; } = new ResultValidation();
        public TicketData ticket { get; set; } = new TicketData();
        public EmployeeData employee { get; set; } = new EmployeeData();

    }
}

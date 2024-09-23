using DataAccess;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class EmployeeLogic
    {
        public DbContextController _controller;
        public EmployeeLogic(DbContextController controller)
        {
            _controller = controller;
        }

        public List<TicketData> GetTickets()
        {
            return _controller.getTickets();
        }
        //public void SetEmployeeId(UserData user)
        //{
        //    _controller.setEmployeeId();
        //}
        public TicketData GetTicketById(int ticketId)
        {
            return _controller.getTicketById(ticketId);
        }
        public UserData GetUserById(int Id)
        {
            return _controller.GetUserData(Id);
        }
        public TicketData AssignTicket(TicketData ticket, int employeeId)
        {
            _controller.AssignTicket(employeeId, ticket.Id);
            return ticket;
        }
        public List<EmployeeData> GetEmployeesInfo()
        {
            return _controller.getEmployeesInfo();
        }
        public EmployeeData GetEmployeeInfo(int Id)
        {
            return _controller.getEmployeeInfo(Id);
        }
        public TicketData ChangeToSolved(int ticketId)
        {
            var ticket = _controller.getTicketById(ticketId);
            _controller.changeToSolved(ticketId);
            ticket.Status = "Solved";
            return ticket;
        }
    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ManagerLogic
    {
        private DbContextController _controller;

        public ManagerLogic(DbContextController controller)
        {
            _controller = controller;
        }
        public List<TicketData> GetTickets()
        {
            return _controller.getTickets();
        }
        public List<ManagerData> GetManagerInfo()
        {
            return _controller.GetManagerInfo();
        }
        public List<ManagerData> GetManagerInfo(string title)
        {
            return _controller.GetManagerInfo(title);
        }
        public void DeleteTicket(int Id)
        {
            _controller.deleteTicket(Id);
        }
        public UserData GetUserInfoById(int EmployeeId)
        {
            return _controller.GetUserData(EmployeeId);
        }
        public TicketData GetTicketById(int ticketId)
        {
            return _controller.getTicketById(ticketId);
        }
        public List<TicketData> GetTicketsByTitle(string title)
        {
            return _controller.getTicketsByTitle(title);
        }

    }
}

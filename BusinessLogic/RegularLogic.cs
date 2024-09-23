using DataAccess;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RegularLogic
    {
        public DbContextController _controller;
        
        public RegularLogic( DbContextController controller)
        {
            _controller = controller;
        }

        public void AddTicket(TicketData ticket)
        {
            _controller.AddTicket(ticket.Title,ticket.Description,ticket.CreatedAt,ticket.Status,ticket.UserId);
        }
        public UserData GetUserById(int Id)
        {
            return _controller.GetUserData(Id);
        }
        public List<TicketData> GetTickets()
        {
            return _controller.getTickets();
        }
        public List<TicketData> GetUserTickets(int userId)
        {
            return _controller.getUserTickets(userId);
        }
    }
}

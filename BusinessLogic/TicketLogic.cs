using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TicketLogic
    {
        public DbContextController _controller;

        public TicketLogic(DbContextController controller)
        {
            _controller = controller;
        }


        public void AddTicket(TicketData ticket)
        {
            _controller.AddTicket(ticket.Title, ticket.Description, ticket.CreatedAt, ticket.Status, ticket.UserId);
        }
        
        public TicketData GetTicketById(int ticket_id)
        {
            return _controller.getTicketById(ticket_id);
        }

        //public void AssignTicketToEmployee(int ticketId, int employeeId)
        //{
        //    var ticket = GetTicketById(ticketId);

        //    if (ticket != null)
        //    {
        //        ticket.EmployeeId = employeeId;
        //        ticket.Id = employeeId;
        //    }
        //}

        public void UpdateTicket(TicketData ticket)
        {
            var existing_ticket = GetTicketById(ticket.Id);

            if (existing_ticket != null)
            {
                existing_ticket.Status = ticket.Status;
                existing_ticket.ResolvedAt = ticket.ResolvedAt;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeData
    {
        public int? EmployeeId { get; set; }  // The employee assigned to this ticket
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public int TicketId { get; set; }
    }
}

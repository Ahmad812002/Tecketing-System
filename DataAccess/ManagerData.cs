using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ManagerData
    {
        public TicketData ticket ;
        public int UserId;
        public int TicketId;
        public int? EmployeeId;
        public int UserType;
        public string Status;
        public DateTime CreatedAt;
        public DateTime? ResolvedAt;
        public string Username;
    }
}

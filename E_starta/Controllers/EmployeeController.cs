using BusinessLogic;
using DataAccess;
using E_starta.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using static DataAccess.TicketData;

namespace E_starta.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeLogic _logic;


        public EmployeeModel _model = new EmployeeModel();
        public EmployeeController(EmployeeLogic logic)
        {
            _logic = logic;
            _model.tickets = _logic.GetTickets();
            _model.employees = _logic.GetEmployeesInfo();
            
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (TempData["ticket"] != null)
            {
                _model.result.ErrorMessage = TempData["ticket"].ToString();
            }
            if (TempData["Message"] != null)
            {
                _model.result.Message = TempData["Message"].ToString();
            }
            if (TempData["Error"] != null)
            {
                _model.result.ErrorMessage = TempData["Error"].ToString();
            }

            _model.ticket.Id = (int)HttpContext.Session.GetInt32("employeeId");
            return View(_model);
        }
        public IActionResult ShowTicketDetails(string Status)
        {
            _model.ticket.Status = Status;
            return View("Index", _model);
        }

        [HttpPost]
        public IActionResult AssignTicket(int ticketId)
        {
            // Fetch the ticket and employee
            var ticket = _logic.GetTicketById(ticketId);
            var employeeId = HttpContext.Session.GetInt32("employeeId");

            if (employeeId == null || employeeId <= 0)
            {
                TempData["Error"] = "Can't find the employee";
                return RedirectToAction("Index");
            }

            var employee = _logic.GetUserById((int)employeeId);

            if (ticket == null)
            {
                TempData["Error"] = "Ticket not found.";
                return RedirectToAction("Index");
            }
            if (employee == null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction("Index");
            }

            // Perform validation and update ticket status
            if (ticket.Status == "InProgress")
            {
                if (ticket.EmployeeId == employeeId)
                {
                    TempData["Error"] = "You have already assigned this ticket.";
                }
                else
                {
                    TempData["Error"] = "This ticket is already taken by someone else.";
                }
            }
            else if (ticket.Status == "Closed")
            {
                TempData["Error"] = "This ticket is closed.";
            }
            else if (ticket.Status == "Solved") TempData["Error"] = "This Ticket is already solved";

            else if (ticket.Status == "Open")
            {
                if (employee.UserType != 2)
                {
                    TempData["Error"] = "You cannot assign tickets.";
                }
                else
                {
                    _logic.AssignTicket(ticket, (int)employeeId);
                    TempData["Message"] = "Ticket assigned successfully.";
                }
            }

            // Redirect to Index action to show updated results
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Solved(int ticketId, int employeeId)
        {

            var ticket = _logic.GetTicketById(ticketId);
            if(ticket.EmployeeId != employeeId) TempData["Error"] = "You have to assign the ticket before marking it as solved.";
            if (ticket.Status == "Solved") TempData["Error"] = "This ticket is already solved";
            if (ticket.Status == "InProgress" && ticket.EmployeeId != employeeId) TempData["Error"] = "This ticket is assigned by someone else";
            else if(ticket.Status == "InProgress" && ticket.EmployeeId == employeeId)
            {
                ticket = _logic.ChangeToSolved(ticketId);
                _model.ticket = ticket;
                TempData["Message"] = "Marked as solved";
            }


            return RedirectToAction("Index");
        }
    }
}

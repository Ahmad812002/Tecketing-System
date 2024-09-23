using BusinessLogic;
using DataAccess;
using E_starta.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_starta.Controllers
{
    public class ManagerController : Controller
    {

        private ManagerLogic _logic;
        public ManagerModel _model ;
        

        public ManagerController(ManagerLogic logic)
        {
            _logic = logic;
            _model = new ManagerModel();
            _model.tickets = _logic.GetTickets();
            _model.manager = _logic.GetManagerInfo();
        }

        public IActionResult Index()
        {
            if (TempData["message"] != null) 
                _model.result.Message = TempData["message"].ToString();
            if (TempData["error"] != null)
                _model.result.ErrorMessage = TempData["error"].ToString();


            return View("Index", _model);
        }


        [HttpPost]
        public IActionResult DeleteTicket(int ticketId, int employeeId)
        {
            _logic.DeleteTicket(ticketId);
            TempData["message"] = "Deleted successfully";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Search(int ticketId)
        {
            if (ticketId <= 0 || ticketId == null) 
            {
                TempData["error"] = "Enter a real ticket Id";
                ViewBag.ShowModal = false;  
                return RedirectToAction("Index"); 
            }
            
            _model.ticket = _logic.GetTicketById(ticketId);
            if (_model.ticket == null)
            {
                TempData["error"] = "Ticket not found";
                ViewBag.ShowModal = false;
                return View("Index", _model);
            }

            var UserId = _model.ticket.UserId;
            _model.user = _logic.GetUserInfoById(UserId);

            var EmployeeId = _model.ticket.EmployeeId;
            if (EmployeeId == null || EmployeeId <= 0)
            {
                TempData["error"] = "Employee is null";
                return RedirectToAction("Index");
            }
            else
            {
                _model.employee = _logic.GetUserInfoById((int)EmployeeId);
            }

            ViewBag.ShowModal = true; // Flag to show modal
            return View("Index", _model);
        }
    }
}

using BusinessLogic;
using DataAccess;
using E_starta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_starta.Controllers
{
    public class RegularController : Controller
    {
        private RegularLogic _logic;
        public ResultValidation _result;

        public RegularModel _model {  get; set; }

        public RegularController(RegularLogic logic)
        {
            _logic = logic;
            _model = new RegularModel()
            {
                users = _logic._controller.GetUsers(),
            };
            _result = new ResultValidation();

        }
        public IActionResult Index()
        {


            _model.tickets = _logic.GetUserTickets((int)HttpContext.Session.GetInt32("userId"));
            if (TempData["ticket"] != null)
            {
                _model.result.ErrorMessage = TempData["ticket"].ToString();
            }
            if (TempData["Message"] != null)
            {
                _model.result.Message = TempData["Message"].ToString();
            }
            if (TempData["Error"] != null) _model.result.ErrorMessage = TempData["Error"].ToString();
            _model.user.Id =(int)HttpContext.Session.GetInt32("userId");

            return View(_model);
        }

        public IActionResult CreateTicket(string Title, string Description)
        {
            var ticket = new TicketData();

            if ((int)HttpContext.Session.GetInt32("userId") == null) TempData["Error"] = "there is no employee to assign";
            else ticket.UserId = (int) HttpContext.Session.GetInt32("userId");



            if(Title == null || Description == null)
            {
                TempData["Error"] = "fill the blocks";
                return RedirectToAction("Index");
            }
            else
            {
                ticket.Description = Description;
                ticket.CreatedAt = DateTime.Now;
                ticket.Status = "Open";
                ticket.Title = Title;
            }
            


            if (ticket == null) TempData["Error"] = "The ticket is null";
            else
            {
                _logic.AddTicket(ticket);
                _model.ticket = ticket;
                _model.tickets = _logic.GetTickets();
                TempData["Message"] = "Created sucessfully";
            }



            return RedirectToAction("Index");
        }
    }
}

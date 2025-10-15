namespace EventMenegmentUI.Controllers
{
    using System.Threading.Tasks;
    using EventMenegmentSL.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;


    public class EventsController : Controller
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllEventWithIncludes();

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {

            var data = await _service.GetByIdEventWithIncludes(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
            
        }
    }

}

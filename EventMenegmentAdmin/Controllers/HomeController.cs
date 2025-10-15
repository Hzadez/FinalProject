using System.Diagnostics;
using EventMenegmentAdmin.Models;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IEventService _eventService;
        
        private readonly ILocationService _locationService;
        private readonly IEventTypeSevice _eventTypeService;
        public HomeController(ILogger<HomeController> logger, IEventService eventService, ILocationService locationService, IEventTypeSevice eventTypeService)
        {
            _logger = logger;
            _eventService = eventService;
            _locationService = locationService;
            _eventTypeService = eventTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventWithIncludes();
            var last3Events = events
    .OrderByDescending(e => e.Date)  
    .Take(3)
    .ToList();

            var locations = await _locationService.GetAllAsync();
          
            var eventTypes =await _eventTypeService.GetAllAsync();
            var model = new HomeViewModel
            {
                Events = last3Events.ToList(),
                Locations = locations.ToList(),
                EventTypes = eventTypes.ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

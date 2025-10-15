using System.Threading.Tasks;
using EventMenegmentSL.Services.Implementation;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentUI.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IEventService _eventService;

        public LocationController(ILocationService locationService, IEventService eventService)
        {
            _locationService = locationService;
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _locationService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var location = await _locationService.GetByIdWithIncludesAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            var eventsAtLocation = await _eventService.GetAllEventWithIncludes();
            var res = eventsAtLocation
                .Where(e => e.Location != null && e.Location.Id == id)
                .ToList();
            var viewModel = new LocationDetailViewModel
            {
                Location = location,
                Events = res
            };

            return View(viewModel);
        }
    }
}

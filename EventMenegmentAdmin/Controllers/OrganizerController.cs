using System.Threading.Tasks;
using EventMenegmentSL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentUI.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _organizerService.GetAllProductWithIncludes();
            return View(data);
        }


        public async Task<IActionResult> Details(int id)
        {
            var organizer = await _organizerService.GetByIdProductWithIncludes(id);
            if (organizer == null)
            {
                return NotFound();
            }
            return View(organizer);
        }
    }
}

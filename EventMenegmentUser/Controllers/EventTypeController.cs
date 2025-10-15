using EventMenegmentDL.Entity;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentAdmin.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly IEventTypeSevice _eventtyp;
        private readonly IGenericService<EventTypeViewModel, EventType> _genericService;

        public EventTypeController(IEventTypeSevice eventtyp, IGenericService<EventTypeViewModel, EventType> genericService)
        {
            _eventtyp = eventtyp;
            _genericService = genericService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _eventtyp.GetAllEventTypeWithIncludes();
            return View(data);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var data = await _genericService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventTypeViewModel eventType)
        {
            if (!ModelState.IsValid)
            {
                return View(eventType);
            }
            var data = _eventtyp.UpdateAsync(eventType);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventTypeViewModel eventType)
        {
            if (ModelState.IsValid)
            {
                var createdEventType = await _eventtyp.AddAsync(eventType);
                if (createdEventType == null)
                {
                    return BadRequest("Failed to create event type.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventType);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _genericService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}

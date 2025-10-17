using EventMenegmentAdmin.DTO;
using EventMenegmentDL.Entity;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentAdmin.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _en;
        private readonly IGenericService<EventViewModel, Event> _generic;
        private readonly ILocationService _location;
        private readonly IOrganizerService _organizer;
        private readonly IEventTypeSevice _type;
        private readonly INotificationService _notificationService;
        private readonly UserManager<AppUser> _userManager;


        public EventController(IEventService eventService, IWebHostEnvironment en, IGenericService<EventViewModel, Event> generic, ILocationService location, IOrganizerService organizer, IEventTypeSevice type, INotificationService notificationService, UserManager<AppUser> userManager)
        {
            _eventService = eventService;
            _en = en;
            _generic = generic;
            _location = location;
            _organizer = organizer;
            _type = type;
            _notificationService = notificationService;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var eventViewModels = await _generic.GetAllAsync();


            var eventDTOs = eventViewModels.Select(ev => new EventDTO
            {
                Id = ev.Id,
                Name = ev.Name,
                Description = ev.Description,
                Date = ev.Date,
                ImageUrl = ev.ImageUrl,
                EventTypeId = ev.EventTypeId,
                LocationId = ev.LocationId,
                OrganizerId = ev.OrganizerId,

            }).ToList();

            return View(eventDTOs);
        }



        public async Task<IActionResult> Details(int id)
        {
            var data = await _eventService.GetByIdEventWithIncludes(id);


            var eventDTO = new EventDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Date = data.Date,
                ImageUrl = data.ImageUrl,
                EventTypeId = data.EventTypeId,
                LocationId = data.LocationId,
                OrganizerId = data.OrganizerId,
                Participants = data.Participants,
                Organizer = data.Organizer,
                Location = data.Location,
                EventType = data.EventType



            };

            return View(eventDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var organizers = await _organizer.GetAllAsync();
            var locations = await _location.GetAllAsync();
            var type = await _type.GetAllAsync();

            var viewModel = new EventDTO
            {
                Locations = locations.Select(m => new LocationViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                Organizers = organizers.Select(m => new OrganizerViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                EventTypes = type.Select(m => new EventTypeViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()


            };

            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(EventDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.Image.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("Image", "Please upload a valid image file.");
                return View(model);
            }

            if (model.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image size must be less than 2MB.");
                return View(model);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
            var uploadsFolder = Path.Combine(_en.WebRootPath, "uploads");

            Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            var dto = new EventViewModel
            {
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                EventTypeId = model.EventTypeId,
                LocationId = model.LocationId,
                OrganizerId = model.OrganizerId,
                Participants = model.Participants,
                ImageUrl = fileName
            };

            await _eventService.AddAsync(dto);
            var usrs =  _userManager.Users.ToList();
            foreach (var item in usrs)
            {
            var token = await _userManager.GenerateUserTokenAsync(item, "Default", "EventNotification");
                var email = item.Email;
               var subject = "New Event Created";
                var message = $"A new event '{model.Name}' has been created. Check it out here: https://localhost:7232/Events";
               
                await _notificationService.SendToAllUsersAsync(email, subject, message);
                

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _generic.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            var organizers = await _organizer.GetAllAsync();
            var locations = await _location.GetAllAsync();
            var type = await _type.GetAllAsync();
            var viewModel = new EventDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Date = data.Date,
                ImageUrl = data.ImageUrl,
                EventTypeId = data.EventTypeId,
                LocationId = data.LocationId,
                OrganizerId = data.OrganizerId,
                Locations = locations.Select(m => new LocationViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                Organizers = organizers.Select(m => new OrganizerViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                EventTypes = type.Select(m => new EventTypeViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EventDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.Image != null && !model.Image.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("Image", "Please upload a valid image file.");
                return View(model);
            }
            if (model.Image != null && model.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image size must be less than 2MB.");
                return View(model);
            }
            var dto = new EventViewModel
            {
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                EventTypeId = model.EventTypeId,
                LocationId = model.LocationId,
                OrganizerId = model.OrganizerId,
                Participants = model.Participants,
                Id = model.Id,
                ImageUrl = model.ImageUrl
            };

            if (model.Image != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                var uploadsFolder = Path.Combine(_en.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                dto.ImageUrl = fileName;
            }

            await _eventService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid event ID.");
            }
            var result = await _generic.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Event not found.");
            }
            return RedirectToAction("Index");
        }
    }
}

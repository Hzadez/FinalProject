using EventMenegmentAdmin.DTO;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentAdmin.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _organizer;
        private readonly IWebHostEnvironment _web;

        public OrganizerController(IOrganizerService organizer, IWebHostEnvironment web)
        {
            _organizer = organizer;
            _web = web;
        }

        public async Task<IActionResult> Index()
        {

            var data = await _organizer.GetAllProductWithIncludes();
            var result = data.Select(o => new OrganizerDTO
            {
                Id = o.Id,
                Name = o.Name,
                Surname = o.Surname,
                Email = o.Email,
                Events = o.Events,
                ImageUrl = o.ImageUrl,
            }).ToList();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(OrganizerDTO model)
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
            var uploadsFolder = Path.Combine(_web.WebRootPath, "uploads");

            Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }
            var viewModel = new OrganizerViewModel
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Events = model.Events,

                ImageUrl = fileName
            };

            var createdOrganizer = await _organizer.AddAsync(viewModel);
            return RedirectToAction("Index", "Organizer");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {


            var data = await _organizer.GetByIdProductWithIncludes(id);
            if (data == null)
            {
                return NotFound();
            }
            var result = await _organizer.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

    }
}


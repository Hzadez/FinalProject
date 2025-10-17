using AutoMapper;
using EventMenegmentAdmin.DTO;
using EventMenegmentSL.Services.Implementation;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace EventMenegmentAdmin.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _organizer;
        private readonly IWebHostEnvironment _web;
        private readonly IMapper _mapper;

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
 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var o = await _organizer.GetByIdAsync(id);
            if (o == null) return NotFound();

            var dto = new OrganizerDTO
            {
                Id = o.Id,
                Name = o.Name,
                Surname = o.Surname,
                Email = o.Email,
                Events = o.Events,         
                ImageUrl = o.ImageUrl                                    
            };

            return View(dto); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrganizerDTO model)
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
            var dto = new OrganizerViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                IventId = model.IventId
            };
            if (model.Image != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                var uploadsFolder = Path.Combine(_web.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                dto.ImageUrl = fileName;
            }

            await _organizer.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

    }
}


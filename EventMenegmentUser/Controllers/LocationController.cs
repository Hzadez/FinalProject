using EventMenegmentAdmin.DTO;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentAdmin.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _location;
        private readonly IWebHostEnvironment _web;

        public LocationController(ILocationService location, IWebHostEnvironment web)
        {
            _location = location;
            _web = web;
        }

        public async Task<IActionResult> Index()
        {
            ; var data = await _location.GetAllAsync();
            var RESULT = data.Select(l => new LocationDTO
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                Address = l.Address,
                Capacity = l.Capacity,
                ImageUrl = l.ImageUrl


            }).ToList();
            return View(RESULT);
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _location.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            var result = new LocationDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Address = data.Address,
                Capacity = data.Capacity,
                ImageUrl = data.ImageUrl
            };
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationDTO model)
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

            var dto = new LocationViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Capacity = model.Capacity,
                ImageUrl = fileName
            };

            await _location.AddAsync(dto);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _location.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            var result = await _location.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _location.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            var result = new LocationDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Address = data.Address,
                Capacity = data.Capacity,
                ImageUrl = data.ImageUrl
            };
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LocationDTO model)
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
            var dto = new LocationViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Capacity = model.Capacity
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

            await _location.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
    }
}

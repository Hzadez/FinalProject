using EventMenegmentDL.Entity;

using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventMenegmentAdmin.Controllers
{

    public class InvitationController : Controller
    {
        private readonly IInvitationService _invitationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEventService _eventService;
        private readonly IUserInivitationService _userInivitationService;

        public InvitationController(IInvitationService invitationService, UserManager<AppUser> userManager, IEventService eventService, IUserInivitationService userInivitationService)
        {
            _invitationService = invitationService;
            _userManager = userManager;
            _eventService = eventService;
            _userInivitationService = userInivitationService;
        }

        public async Task<IActionResult> Create()
        {
            var userList = _userManager.Users.ToList();
            var eventList = await _eventService.GetAllAsync();

            var model = new InvitationViewModel
            {
                Users = userList,
                Events = eventList
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvitationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = _userManager.Users.ToList();
                model.Events = await _eventService.GetAllAsync();
                return View(model);
            }

           await _invitationService.CreateInvitationWithUsersAsync(model);


            return RedirectToAction("Index", "Home");
        }




    }



}

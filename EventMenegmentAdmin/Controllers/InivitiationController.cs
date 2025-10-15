using EventMenegmentDL.Entity;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using EventMenegmentUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentUI.Controllers
{
    public class InivitiationController : Controller
    {
       private readonly IUserInivitationService _userInivitationService;
        
        public InivitiationController(IUserInivitationService userInivitationService)
        {
            _userInivitationService = userInivitationService;

            
        }


        public async Task<IActionResult> Index(string UserId)
        {
           var data = await _userInivitationService.GetByIdUserInivitation(UserId);

            var result =data.Select(ui=> new InivitationAcceptViewModel
            {
                Id= ui.Id,
                Description= ui.Description,
                Title= ui.Title,
                EventDate= ui.EventDate,
                EventLocation= ui.Location.Name,
                EventName= ui.EventName,
                


            }).ToList();
         

            return View(result);
        }
        public async Task<IActionResult> Accept(int id )
        {
            var data = await _userInivitationService.GetByInvitationIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            var result =await _userInivitationService.UpdateUserAcceptInivitation(data);

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Decline(int id)
        {
            var data = await _userInivitationService.GetByInvitationIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            
            var result = await _userInivitationService.UpdateUserDeclineInivitation(data);
            return RedirectToAction("Index", "Home");

        }

  
    }
}

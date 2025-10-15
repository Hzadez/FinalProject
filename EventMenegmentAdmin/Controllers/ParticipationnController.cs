using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Implementation;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Implementation;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentUI.Controllers
{
    public class ParticipationnController : Controller
    {

        private readonly IParticipationService _participationService;
        private readonly IUserInivitationService _userInivitationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IQrCodeService _qrCodeService;
        private readonly INotificationService _emailService;
        public ParticipationnController(IParticipationService participationService, IUserInivitationService userInivitationService, UserManager<AppUser> userManager, IQrCodeService qrCodeService, INotificationService emailService)
        {

            _participationService = participationService;
            _userInivitationService = userInivitationService;
            _userManager = userManager;
            _qrCodeService = qrCodeService;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _participationService.GetAllProductWithIncludes();
            return View(data);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(string userId)
        {
            var existing = await _participationService.GetParticipationsByUserId(userId);
            var data = await _userInivitationService.GetByInivitationForParticipation(userId);
            var user = await _userManager.FindByIdAsync(userId);    

            foreach (var item in data)
            {
                if (item.InvitationId == 0)
                    continue;

                bool alreadyExists = existing.Any(p => p.InvitationId == item.InvitationId);
                if (alreadyExists)
                    continue;

                var participation = new ParticipationViewModel
                {
                    UserId = userId,
                    InvitationId = item.InvitationId,
                    CheckInTime = item.EventDate,
                    LocationName = item.Location.Name,
                    SeatNumber = new Random().Next(1, item.Capacity),
                };

                await _participationService.AddAsync(participation);
                existing.Add(participation);

                string qrText = qrText = $"https://EvenX.az/participation";

                
                var qrImage = _qrCodeService.GenerateQrCode(qrText);

             
                string emailBody = $"Salam, siz {item.EventName} tədbirinə uğurla qeydiyyatdan keçdiniz. QR kod əlavə edilmişdir.";
                await _emailService.SendEmailWithQrAsync(
                    toEmail: user.Email,
                    subject: "Tədbir Qeydiyyatı və QR Kod",
                    body: emailBody,
                    qrImage: qrImage,
                    fileName: "participation-qrcode.png"
                );
            }

            return RedirectToAction("Index", "Participationn");
        }

    }
}

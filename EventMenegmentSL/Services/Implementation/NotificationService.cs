
using System.Net;
using System.Net.Mail;
using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class NotificationService : GenericService<NotificationViewModel, Notification>, INotificationService
    {
        private readonly IGenericRepository<Notification> _notificationRepo;
       
        private readonly IMapper _mapper;
      

        public NotificationService(IGenericRepository<Notification> notificationRepo, IMapper mapper) : base(mapper, notificationRepo)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
           
        
        }

        public async Task SendEmailWithQrAsync(string toEmail, string subject, string body, byte[] qrImage, string fileName)
        {
            var fromAddress = new MailAddress("zeynaleh@code.edu.az");
            var toAddress = new MailAddress(toEmail);
            const string password = "nemw wxgk vlzm lksz"; 

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromAddress.Address, password),
                EnableSsl = true
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            if (qrImage != null)
            {
                var ms = new MemoryStream(qrImage);
                var attachment = new Attachment(ms, fileName, "image/png");
                message.Attachments.Add(attachment);
            }

            await smtp.SendMailAsync(message);
        }

        public async Task SendToAllUsersAsync(string email, string subject, string message)
        {
            // 1. Email boşdursa və ya səhv formatdadırsa, göndərmə
            if (string.IsNullOrWhiteSpace(email))
                return;

            email = email.Trim();
            if (!MailAddress.TryCreate(email, out var toAddress))
                return; // düzgün formatda deyilsə, atla

            // 2. SMTP müştərisini qur
            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    "zeynaleh@code.edu.az",        // GMAIL adresin
                    "nemw wxgk vlzm lksz"          // App Password (2FA üçün)
                )
            };

            // 3. Mesajı hazırla
            var mail = new MailMessage
            {
                From = new MailAddress("zeynaleh@code.edu.az", "Event Manager"),
                Subject = subject,
                Body = message,
                IsBodyHtml = false
            };

            mail.To.Add(toAddress);

            // 4. Göndər
            await client.SendMailAsync(mail);
        }
    }
}

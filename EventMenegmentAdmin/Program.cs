using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Implementation;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Implementation;
using EventMenegmentSL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace EventMenegmentUser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(GenericRepository<>), typeof(EventMenegmentDL.Repository.Implementation.GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            builder.Services.AddScoped<IEventTypeSevice, EventTypeService>();
            builder.Services.AddAutoMapper(typeof(EventMenegmentSL.Profiles.CustomProfile));
            builder.Services.AddScoped<IInvitationService, InvitiationService>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IOrganizerRepository, OrganizeRepository>();
            builder.Services.AddScoped<IOrganizerService, OrganizerService>();
            builder.Services.AddScoped<IUserInivitationRepository, UserInivitationRepository>();
            builder.Services.AddScoped<IUserInivitationService, UserInvitationService>();
            builder.Services.AddScoped<IParticipationRepository, ParticipationRepository>();
            builder.Services.AddScoped<IParticipationService, ParticipationService>();
            builder.Services.AddScoped<IQrCodeService, QrCodeService>();


            builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();



            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Admin layihəsinin wwwroot/uploads qovluğunu göstəririk
            var adminUploads = Path.GetFullPath(
                Path.Combine(builder.Environment.ContentRootPath, "..", "EventMenegmentUser", "wwwroot", "uploads")
            );

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(adminUploads),
                RequestPath = "/uploads", // UI-da çağırış üçün prefiks
                ServeUnknownFileTypes = true
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

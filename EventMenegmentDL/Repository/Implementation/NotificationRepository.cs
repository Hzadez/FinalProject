using EventMenegmentDL.Data;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentDL.Repository.Implementation
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddRangeAsync(List<Notification> notifications)
        {
            await _context.Notifications.AddRangeAsync(notifications);
        }
        public async Task<List<Notification>> GetAllNotificationWithIncludes()
        {
            var data = await _context.Notifications.Include(n => n.Event).ToListAsync();
            return data;
        }

        public async Task<Notification> GetByIdNotificationWithIncludes(int id)
        {
            var data = await _context.Notifications.Include(n => n.Event).FirstOrDefaultAsync(n => n.Id == id);
            return data;
        }

        public async Task<Notification> UpdateAsync(Notification entity)
        {
            var existingNotification = await _context.Notifications.FindAsync(entity.Id);
            if (existingNotification != null)
            {
                _context.Update(existingNotification);
                await _context.SaveChangesAsync();
                return existingNotification;
            }
            return null;
        }
    }
}

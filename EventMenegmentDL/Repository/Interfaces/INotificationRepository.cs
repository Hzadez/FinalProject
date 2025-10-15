using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<List<Notification>> GetAllNotificationWithIncludes();
        Task<Notification> GetByIdNotificationWithIncludes(int id);
        Task AddRangeAsync(List<Notification> notifications);

        Task<Notification> UpdateAsync(Notification entity);
    }
}

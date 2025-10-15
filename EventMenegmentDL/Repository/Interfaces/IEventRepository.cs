using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllEventWithIncludes();
        Task<Event> GetEventWithIncludes(int id);
        Task<Event> UpdateAsync(Event entity);

    }

}

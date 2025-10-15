using EventMenegmentDL.Entity;

namespace EventMenegmentDL.Repository.Interfaces
{
    public interface IEventTypeRepository : IGenericRepository<EventType>
    {

        Task<EventType> UpdateAsync(EventType employee);


    }
}

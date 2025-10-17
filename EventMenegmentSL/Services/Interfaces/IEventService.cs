using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IEventService : IGenericService<EventViewModel, Event>
    {
        public Task<List<EventViewModel>> GetAllEventWithIncludes();
        public Task<EventViewModel> GetByIdEventWithIncludes(int id);
        public Task<EventViewModel> UpdateAsync(EventViewModel entity);
    }
}

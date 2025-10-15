

using EventMenegmentDL.Entity;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Interfaces
{
    public interface IEventTypeSevice : IGenericService<EventTypeViewModel, EventType>
    {
        public Task<List<EventTypeViewModel>> GetAllEventTypeWithIncludes();
        public Task<EventTypeViewModel> GetEventTypeWithIncludes(int id);
        public Task<EventTypeViewModel> UpdateAsync(EventTypeViewModel entity);


    }
}

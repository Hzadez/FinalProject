
using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class EventService : GenericService<EventViewModel, Event>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository, IMapper mapper)
       : base(mapper, eventRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }





        public async Task<List<EventViewModel>> GetAllEventWithIncludes()
        {
            var data = await _eventRepository.GetAllEventWithIncludes();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<List<EventViewModel>>(data);
            return result;
        }

        public async Task<EventViewModel> GetByIdEventWithIncludes(int id)
        {
            var data = await _eventRepository.GetEventWithIncludes(id);
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<EventViewModel>(data);
            return result;
        }

        public async Task<EventViewModel> UpdateAsync(EventViewModel entity)
        {
            var entityToUpdate = _mapper.Map<Event>(entity);
            var result = await _eventRepository.UpdateAsync(entityToUpdate);
            if (result == null)
            {
                return null;
            }
            var updatedResult = _mapper.Map<EventViewModel>(result);
            return updatedResult;
        }
    }
}

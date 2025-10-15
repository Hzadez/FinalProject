

using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class EventTypeService : GenericService<EventTypeViewModel, EventType>, IEventTypeSevice
    {
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IMapper _mapper;
        public EventTypeService(IMapper mapper, IEventTypeRepository repository) : base(mapper, repository)
        {
            _eventTypeRepository = repository;
            _mapper = mapper;
        }




        public async Task<List<EventTypeViewModel>> GetAllEventTypeWithIncludes()
        {
            var eventTypes = await _eventTypeRepository.GetAllAsync();
            return _mapper.Map<List<EventTypeViewModel>>(eventTypes);

        }

        public async Task<EventTypeViewModel> GetEventTypeWithIncludes(int id)
        {
            var data = await _eventTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<EventTypeViewModel>(data);

        }

        public async Task<EventTypeViewModel> UpdateAsync(EventTypeViewModel entity)
        {
            var eventTypeToUpdate = _mapper.Map<EventType>(entity);
            var updatedEventType = await _eventTypeRepository.UpdateAsync(eventTypeToUpdate);
            if (updatedEventType == null)
            {
                return null;
            }
            return _mapper.Map<EventTypeViewModel>(updatedEventType);
        }
    }
}

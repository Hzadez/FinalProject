using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class LocationService : GenericService<LocationViewModel, Location>, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper) : base(mapper, locationRepository)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }




         

        public async Task<LocationViewModel> UpdateAsync(LocationViewModel entity)
        {
            var data = _mapper.Map<Location>(entity);
            var result = await _locationRepository.UpdateAsync(data);
            if (result == null)
            {
                return null;
            }
            var updatedResult = _mapper.Map<LocationViewModel>(result);
            return updatedResult;

        }

      public async  Task<LocationViewModel>GetByIdWithIncludesAsync(int id)
        {
           var data = await _locationRepository.GetByIdWithIncludes(id);
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<LocationViewModel>(data);
            return result;
        }
    }
}

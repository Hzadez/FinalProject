
using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class OrganizerService : GenericService<OrganizerViewModel, Organizer>, IOrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IMapper _mapper;

        public OrganizerService(IOrganizerRepository organizerRepository, IMapper mapper) : base(mapper, organizerRepository)
        {
            _organizerRepository = organizerRepository;
            _mapper = mapper;
        }

        public async Task<List<OrganizerViewModel>> GetAllProductWithIncludes()
        {
            var organizers = await _organizerRepository.GetAllOrganizerWithIncludes();
            return _mapper.Map<List<OrganizerViewModel>>(organizers);
        }

        public async Task<OrganizerViewModel> GetByIdProductWithIncludes(int id)
        {
            var organizer = await _organizerRepository.GetByIdOrganizerWithIncludes(id);
            if (organizer == null)
            {
                return null;
            }
            return _mapper.Map<OrganizerViewModel>(organizer);

        }



        public async Task<OrganizerViewModel> UpdateAsync(OrganizerViewModel product)
        {
            var existingOrganizer = await _organizerRepository.GetByIdOrganizerWithIncludes(product.Id);
            if (existingOrganizer == null)
            {
                throw new KeyNotFoundException($"Organizer with ID {product.Id} not found.");
            }
            var updatedOrganizer = _mapper.Map<Organizer>(product);
            var result = await _organizerRepository.Update(updatedOrganizer);
            if (result == null)
            {
                return null;
            }
            return _mapper.Map<OrganizerViewModel>(result);
        }
    }
}

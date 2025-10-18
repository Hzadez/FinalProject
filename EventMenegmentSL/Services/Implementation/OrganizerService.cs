
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



        public async Task<OrganizerViewModel> UpdateAsync(OrganizerViewModel model)
        {
            var existing = await _organizerRepository.GetByIdOrganizerWithIncludes(model.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Organizer with ID {model.Id} not found.");

            // YENİ obyekt yaratma! Mövcud tracked entity-nin ÜZƏRİNƏ map et
            _mapper.Map(model, existing);

            // Repo sadəcə SaveChanges etsin, Update() çağırma
            var saved = await _organizerRepository.Update(existing);

            return _mapper.Map<OrganizerViewModel>(saved);
        }

    }
}

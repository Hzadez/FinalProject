using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class ParticipationService : GenericService<ParticipationViewModel, Participation>, IParticipationService
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IMapper _mapper;

        public ParticipationService(IParticipationRepository participationRepository, IMapper mapper) : base(mapper, participationRepository)
        {
            _participationRepository = participationRepository;
            _mapper = mapper;
        }

        public async Task<List<ParticipationViewModel>> GetAllProductWithIncludes()
        {
            var data = await _participationRepository.GetAllParticipationWithIncludes();
            return _mapper.Map<List<ParticipationViewModel>>(data);
        }

        public async Task<ParticipationViewModel> GetByIdProductWithIncludes(int id)
        {
            var data = await _participationRepository.GetByIdParticipationWithIncludes(id);
            return _mapper.Map<ParticipationViewModel>(data);
        }

        public async Task<List<ParticipationViewModel>> GetParticipationsByUserId(string userId)
        {
            var data = await _participationRepository.GetParticipationsByUserId(userId);
            return _mapper.Map<List<ParticipationViewModel>>(data);
             
        }

        public async Task<ParticipationViewModel> UpdateAsync(ParticipationViewModel product)
        {
            var data = _mapper.Map<Participation>(product);
            var result = await _participationRepository.UpdateAsync(data);
            if (result == null)
            {
                return null;
            }
            var updatedResult = _mapper.Map<ParticipationViewModel>(result);
            return updatedResult;



        }
    }
}

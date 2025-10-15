using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;
using EventMenegmentSL.ViewModel;

namespace EventMenegmentSL.Services.Implementation
{
    public class FeedbackService : GenericService<FeedbackViewModel, Feedback>, IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper) : base(mapper, feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<List<FeedbackViewModel>> GetAllFeedbackWithIncludes()
        {
            var data = await _feedbackRepository.GetAllFeedbackWithIncludes();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<List<FeedbackViewModel>>(data);
            return result;
        }

        public async Task<FeedbackViewModel> GetByIdWithIncludesAsync(int id)
        {
            var data = await _feedbackRepository.GetByIdFeedbackWithIncludes(id);
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<FeedbackViewModel>(data);
            return result;

        }

        public async Task<FeedbackViewModel> UpdateAsync(FeedbackViewModel entity)
        {
            var entityToUpdate = _mapper.Map<Feedback>(entity);
            var result = await _feedbackRepository.UpdateAsync(entityToUpdate);
            if (result == null)
            {
                return null;
            }
            var updatedResult = _mapper.Map<FeedbackViewModel>(result);
            return updatedResult;
        }
    }
}

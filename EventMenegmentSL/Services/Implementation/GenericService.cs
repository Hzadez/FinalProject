using AutoMapper;
using EventMenegmentDL.Entity;
using EventMenegmentDL.Repository.Interfaces;
using EventMenegmentSL.Services.Interfaces;

namespace EventMenegmentSL.Services.Implementation
{
    public class GenericService<TVM, TEntity> : IGenericService<TVM, TEntity> where TVM : class where TEntity : BaseEntity, new()
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IMapper mapper, IGenericRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }




        public async Task<TVM> AddAsync(TVM entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entityToAdd = _mapper.Map<TEntity>(entity);
            var result = await _repository.AddAsync(entityToAdd);
            return _mapper.Map<TVM>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;

        }

        public async Task<IEnumerable<TVM>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<IEnumerable<TVM>>(data);
            return result;
        }

        public async Task<TVM> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<TVM>(data);
            return result;
        }


    }
}

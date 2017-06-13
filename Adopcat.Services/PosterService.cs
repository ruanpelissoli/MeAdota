using Adopcat.Services.Interfaces;
using System.Collections.Generic;
using Adopcat.Model;
using Adopcat.Data.Interfaces;
using System.Threading.Tasks;

namespace Adopcat.Services
{
    public class PosterService : BaseService, IPosterService
    {
        private IPosterRepository _repository;

        public PosterService(ILoggingService log, IPosterRepository repository) :base(log)
        {
            _repository = repository;
        }

        public async Task<Poster> CreateAsync(Poster poster)
        {
            return await TryCatch(async () =>
            {
                return await _repository.CreateAsync(poster);
            });            
        }

        public async Task<int> UpdateAsync(Poster poster)
        {
            return await TryCatch(async () =>
            {
                return await _repository.UpdateAsync(poster);
            });
        }

        public async Task<Poster> GetAsync(int id)
        {
            return await TryCatch(async () =>
            {
                return await _repository.FindAsync(id);
            });            
        }

        public async Task<List<Poster>> GetAsync()
        {
            return await TryCatch(async () =>
            {
                return await _repository.GetAllAsync();
            });
        }

        public async Task<List<Poster>> GetByUserIdAsync(int userId)
        {
            return await TryCatch(async () =>
            {
                return await _repository.GetAllAsync(w => w.UserId == userId);
            });            
        }

        public async Task<List<Poster>> GetByStateAsync(string state)
        {
            return await TryCatch(async () =>
            {
                return await _repository.GetAllAsync(w => w.State == state);
            });            
        }

        public async Task<List<Poster>> GetByCityAsync(string city)
        {
            return await TryCatch(async () =>
            {
                return await _repository.GetAllAsync(w => w.City == city);
            });            
        }
    }
}

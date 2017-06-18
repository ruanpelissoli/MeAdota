using Adopcat.Services.Interfaces;
using System.Collections.Generic;
using Adopcat.Model;
using Adopcat.Data.Interfaces;
using System.Threading.Tasks;
using Adopcat.Model.DTO;
using Adopcat.Model.Enums;
using System.Linq;

namespace Adopcat.Services
{
    public class PosterService : BaseService, IPosterService
    {
        private IPosterRepository _repository;
        private IPetPictureService _petPictureService;

        public PosterService(ILoggingService log,
                             IPosterRepository repository,
                             IPetPictureService petPictureService) : base(log)
        {
            _repository = repository;
            _petPictureService = petPictureService;
        }

        public async Task<Poster> CreateAsync(PosterInputDTO posterDto)
        {
            return await TryCatch(async () =>
            {
                var poster = new Poster
                {
                    UserId = posterDto.UserId,
                    PetName = posterDto.PetName,
                    PetType = (EPetType)posterDto.PetType,
                    Castrated = posterDto.Castrated,
                    Dewormed = posterDto.Dewormed,
                    DeliverToAdopter = posterDto.DeliverToAdopter,
                    Country = posterDto.Country,
                    State = posterDto.State,
                    City = posterDto.City,
                    IsAdopted = posterDto.IsAdopted
                };

                poster = await _repository.CreateAsync(poster);

                foreach (var pic in posterDto.PetPictures)
                    await _petPictureService.Create(pic, poster.Id);

                return poster;
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

        public async Task<List<Poster>> GetAllPostersAsync(int userId)
        {
            return await TryCatch(async () =>
            {
                return await _repository.GetAllAsync(w => w.UserId != userId && !w.IsAdopted);
            });
        }

        public async Task<List<Poster>> GetAllPostersAsync(int userId, FilterDTO filter)
        {
            return await TryCatch(async () =>
            {
                var list = await _repository.GetAllAsync(w => w.UserId != userId && !w.IsAdopted);

                if (filter.PetType.HasValue)
                    list = list.Where(w => (int)w.PetType == filter.PetType.Value).ToList();

                if (filter.Castrated.HasValue)
                    list = list.Where(w => w.Castrated == filter.Castrated.Value).ToList();

                if (filter.Dewormed.HasValue)
                    list = list.Where(w => w.Dewormed == filter.Dewormed.Value).ToList();

                if (filter.DeliverToAdopter.HasValue)
                    list = list.Where(w => w.Dewormed == filter.DeliverToAdopter.Value).ToList();

                if(!string.IsNullOrEmpty(filter.City))
                    list = list.Where(w => w.City == filter.City).ToList();

                return list;
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

using Adopcat.Services.Interfaces;
using System.Collections.Generic;
using Adopcat.Model;
using Adopcat.Data.Interfaces;
using System.Threading.Tasks;
using Adopcat.Model.DTO;
using Adopcat.Model.Enums;

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

using Adopcat.Model;
using Adopcat.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IPosterService
    {
        Task<Poster> GetAsync(int id);
        Task<List<Poster>> GetAllPostersAsync(int userId);
        Task<List<Poster>> GetAllPostersAsync(int userId, FilterDTO filter);
        Task<Poster> CreateAsync(PosterInputDTO poster);
        Task UpdateAsync(PosterInputDTO poster);
        Task<List<Poster>> GetByUserIdAsync(int id);
        Task<List<Poster>> GetByStateAsync(string state);
        Task<List<Poster>> GetByCityAsync(string city);
        Task DeleteAsync(int id);
    }
}
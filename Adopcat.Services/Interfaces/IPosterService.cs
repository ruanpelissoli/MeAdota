using Adopcat.Model;
using Adopcat.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IPosterService
    {
        Task<Poster> GetAsync(int id);
        Task<List<Poster>> GetAsync();
        Task<Poster> CreateAsync(PosterInputDTO poster);
        Task<int> UpdateAsync(Poster poster);
        Task<List<Poster>> GetByUserIdAsync(int id);
        Task<List<Poster>> GetByStateAsync(string state);
        Task<List<Poster>> GetByCityAsync(string city);
    }
}
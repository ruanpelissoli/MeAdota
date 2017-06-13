using Adopcat.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IPosterService
    {
        Task<Poster> GetAsync(int id);
        Task<List<Poster>> GetAsync();
        Task<Poster> CreateAsync(Poster poster);
        Task<int> UpdateAsync(Poster poster);
        Task<List<Poster>> GetByUserIdAsync(int id);
        Task<List<Poster>> GetByStateAsync(string state);
        Task<List<Poster>> GetByCityAsync(string city);
    }
}
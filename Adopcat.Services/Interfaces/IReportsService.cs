using Adopcat.Model;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IReportsService
    {
        Task<Reports> CreateAsync(Reports model);
    }
}

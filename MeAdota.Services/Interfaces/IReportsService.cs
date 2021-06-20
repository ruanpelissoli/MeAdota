using MeAdota.Model;
using System.Threading.Tasks;

namespace MeAdota.Services.Interfaces
{
    public interface IReportsService
    {
        Task<Reports> CreateAsync(Reports model);
    }
}

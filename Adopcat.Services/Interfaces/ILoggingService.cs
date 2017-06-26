using Adopcat.Model;
using System;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface ILoggingService
    {
        Task Error(Exception ex);
        Task CreateAsync(SystemLog log);
    }
}

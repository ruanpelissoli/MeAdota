using System;
using System.Threading.Tasks;

namespace MeAdota.Services.Interfaces
{
    public interface ILoggingService
    {
        Task Error(Exception ex);
    }
}

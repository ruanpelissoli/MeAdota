using System;

namespace Adopcat.Services.Interfaces
{
    public interface ILoggingService
    {
        void Error(Exception ex);
    }
}

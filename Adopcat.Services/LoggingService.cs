using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Model.Enums;
using Adopcat.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Adopcat.Services
{
    public class LoggingService : ILoggingService
    {
        private ISystemLogRepository _systemLogRepository;

        public LoggingService(ISystemLogRepository systemLogRepository)
        {
            _systemLogRepository = systemLogRepository;
        }

        public async Task CreateAsync(SystemLog log)
        {
            await _systemLogRepository.CreateAsync(log);
        }

        public async Task Error(Exception ex)
        {
            try
            {
                var error = new SystemLog()
                {
                    LogDate = DateTime.Now,
                    LogType = ELogType.Error,
                    Platform = EPlatform.API,
                    Text = ex.Message,
                };

                await _systemLogRepository.CreateAsync(error);
            }
            catch (Exception) { }
        }
    }
}

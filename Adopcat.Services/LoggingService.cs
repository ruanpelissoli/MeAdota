using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Model.Enums;
using Adopcat.Services.Interfaces;
using System;

namespace Adopcat.Services
{
    public class LoggingService : ILoggingService
    {
        private ISystemLogRepository _systemLogRepository;

        public LoggingService(ISystemLogRepository systemLogRepository)
        {
            _systemLogRepository = systemLogRepository;
        }

        public void Error(Exception ex)
        {
            try
            {
                var error = new SystemLog()
                {
                    LogDate = DateTime.Now,
                    LogType = ELogType.Error,
                    Text = ex.Message,
                };

                _systemLogRepository.Create(error);
            }
            catch (Exception) { }
        }
    }
}

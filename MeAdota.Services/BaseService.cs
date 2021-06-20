using MeAdota.Services.Exceptions;
using MeAdota.Services.Interfaces;
using System;

namespace MeAdota.Services
{
    public class BaseService
    {
        protected ILoggingService _log;

        public BaseService(ILoggingService log)
        {
            _log = log;
        }

        protected T TryCatch<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (UnauthorizedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        protected void TryCatch(Action action)
        {
            try
            {
                action();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (UnauthorizedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }
    }
}

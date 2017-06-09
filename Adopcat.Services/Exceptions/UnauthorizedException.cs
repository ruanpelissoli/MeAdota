using System;

namespace Adopcat.Services.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}

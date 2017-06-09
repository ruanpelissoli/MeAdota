using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using Adopcat.Services.Exceptions;

namespace Adopcat.API.Filters
{
    public class ExceptionHandling : ExceptionFilterAttribute
    {
        private const string _reasonPhrase = "Exception";

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BadRequestException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = _reasonPhrase
                };
            }
            else if (context.Exception is NotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = _reasonPhrase
                };
            }
            else if (context.Exception is BusinessException)
            {
                var exp = (BusinessException)context.Exception;

                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = _reasonPhrase
                };
            }
            else if (context.Exception is UnauthorizedException)
            {
                var exp = (UnauthorizedException)context.Exception;

                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = _reasonPhrase
                };

            }
            else if (context.Exception is UnsupportedMediaTypeException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = _reasonPhrase
                };
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = _reasonPhrase
                };
            }
        }
    }
}
using CinemaSchedule.BusinessLogic.Models.Exceptions;
using CinemaSchedule.Models.ApiModels;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CinemaSchedule.Attributes
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (context.Exception is BusinessException)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new ObjectContent<ErrorWrapperAm>(new ErrorWrapperAm(context.Exception.Message), new JsonMediaTypeFormatter()),
                        ReasonPhrase = "Exception"
                    });
                }

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new ObjectContent<ErrorWrapperAm>(new ErrorWrapperAm("An error occurred, please try again or contact the administrator."), new JsonMediaTypeFormatter()),
                    ReasonPhrase = "Critical Exception"
                });
            }, cancellationToken);
        }
    }
}
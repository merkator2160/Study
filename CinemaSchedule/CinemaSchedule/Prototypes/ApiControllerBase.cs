using CinemaSchedule.BusinessLogic.Models.Exceptions;
using CinemaSchedule.Models.ApiModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CinemaSchedule.Prototypes
{
    public class ApiControllerBase : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            try
            {
                return base.ExecuteAsync(controllerContext, cancellationToken);
            }
            catch (Exception ex)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    if (ex is BusinessException)
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new ObjectContent<ErrorWrapperAm>(new ErrorWrapperAm(ex.Message), new JsonMediaTypeFormatter()),
                            ReasonPhrase = "Exception"
                        };
                    }

                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("An error occurred, please try again or contact the administrator."),
                        ReasonPhrase = "Critical Exception"
                    };
                }, cancellationToken);
            }
        }
    }
}
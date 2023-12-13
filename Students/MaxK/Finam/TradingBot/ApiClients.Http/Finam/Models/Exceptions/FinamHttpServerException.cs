using ApiClientsHttp.Finam.Models.Response;
using Common.Contracts.Exceptions.Application;
using System.Net;

namespace ApiClientsHttp.Finam.Models.Exceptions
{
    public class FinamHttpServerException : HttpServerException
    {
        public FinamHttpServerException(HttpMethod verb, HttpStatusCode statusCode, String uri, String message, FinamErrorResponseApi error) : base(verb, statusCode, uri, message)
        {
            FinamError = error;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public FinamErrorResponseApi FinamError { get; }
    }
}
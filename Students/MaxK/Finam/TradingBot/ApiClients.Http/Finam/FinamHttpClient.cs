using ApiClientsHttp.Finam.Models.Config;
using ApiClientsHttp.Finam.Models.Exceptions;
using ApiClientsHttp.Finam.Models.Response;
using Common.Contracts.Const;
using Common.Http;
using System.Net.Http.Headers;

namespace ApiClientsHttp.Finam
{
    /// <summary>
    /// SWAGGER: https://trade-api.finam.ru/swagger/index.html
    /// </summary>
    public class FinamHttpClient : TypedHttpClient
    {
        private readonly FinamHttpClientConfig _config;


        public FinamHttpClient(FinamHttpClientConfig config)
        {
            _config = config;
            BaseAddress = new Uri(config.BaseAddress);

            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpMimeType.Application.Json));
            DefaultRequestHeaders.Add("X-Api-Key", config.ApiKey);
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public async Task<TokenCheckResponseApi> CheckTokenAsync()
        {
            using (var response = await GetAsync("public/api/v1/access-tokens/check"))
            {
                return await HandleFinamResponse<TokenCheckResponseApi>(HttpMethod.Get, response);
            }
        }
        public async Task<SecurityApi[]> GetSecuritiesAsync()
        {
            using (var response = await GetAsync("public/api/v1/securities"))
            {
                return (await HandleFinamResponse<SecuritiesResponseApi>(HttpMethod.Get, response)).Securities;
            }
        }
        public async Task<PortfolioResponseApi> GetPortfolioAsync()
        {
            using (var response = await GetAsync($"public/api/v1/portfolio/?ClientId={_config.Account}"))
            {
                return await HandleFinamResponse<PortfolioResponseApi>(HttpMethod.Get, response);
            }
        }

        // Orders //
        public async Task<OrderResponseApi> GetOrdersAsync()
        {
            using (var response = await GetAsync($"public/api/v1/orders?ClientId={_config.Account}"))
            {
                return await HandleFinamResponse<OrderResponseApi>(HttpMethod.Get, response);
            }
        }

        // Stops //
        public async Task<StopResponseApi> GetStopsAsync()
        {
            using (var response = await GetAsync($"public/api/v1/stops?ClientId={_config.Account}"))
            {
                return await HandleFinamResponse<StopResponseApi>(HttpMethod.Get, response);
            }
        }
        

        // SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
        private async Task<T> HandleFinamResponse<T>(HttpMethod method, HttpResponseMessage response)
        {
            var message = await DeserializeAsync<FinamResponseApi<T>>(response);
            if (!response.IsSuccessStatusCode)
                throw new FinamHttpServerException(method, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, message.Error.Message, message.Error);

            return message.Data;
        }
    }
}
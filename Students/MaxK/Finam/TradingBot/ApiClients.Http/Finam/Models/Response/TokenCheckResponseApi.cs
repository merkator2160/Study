using Newtonsoft.Json;

namespace ApiClientsHttp.Finam.Models.Response
{
    public class TokenCheckResponseApi
    {
        [JsonProperty("id")]
        public Int32 ApiKeyId { get; set; }
    }
}
using Common.Contracts.Const;
using Common.Contracts.Exceptions.Application;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Common.Http
{
    public class TypedHttpClient : HttpClient
    {
        public TypedHttpClient()
        {
            SerializerSettings = new JsonSerializerSettings();
        }
        public TypedHttpClient(JsonSerializerSettings serializerSettings)
        {
            SerializerSettings = serializerSettings;
        }
        public TypedHttpClient(JsonSerializerSettings serializerSettings, HttpMessageHandler handler) : base(handler)
        {
            SerializerSettings = serializerSettings;
        }
        

        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        protected JsonSerializerSettings SerializerSettings { get; set; }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public async Task<T> GetObjectAsync<T>(String uri)
        {
            using (var response = await GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(HttpMethod.Get, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, await response.Content.ReadAsStringAsync());

                return await DeserializeAsync<T>(response);
            }
        }
        public async Task PostObjectAsync(String uri)
        {
            using (var response = await PostAsync(uri, null))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(HttpMethod.Post, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, await response.Content.ReadAsStringAsync());
            }
        }
        public async Task<T> PostObjectAsync<T>(String uri)
        {
            using (var response = await PostAsync(uri, null))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(HttpMethod.Post, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, await response.Content.ReadAsStringAsync());

                return await DeserializeAsync<T>(response);
            }
        }
        public async Task<T1> PostObjectAsync<T1, T2>(String uri, T2 obj)
        {
            using (var response = await PostAsync(uri, Serialize(obj)))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(HttpMethod.Post, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, await response.Content.ReadAsStringAsync());

                return await DeserializeAsync<T1>(response);
            }
        }
        public async Task PostObjectAsync<T>(String uri, T obj)
        {
            using (var response = await PostAsync(uri, Serialize(obj)))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(HttpMethod.Post, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, await response.Content.ReadAsStringAsync());
            }
        }
        public new async Task DeleteAsync(String uri)
        {
            using (var response = await base.DeleteAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(HttpMethod.Delete, response.StatusCode, response.RequestMessage.RequestUri.AbsoluteUri, await response.Content.ReadAsStringAsync());
            }
        }


        // SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
        protected HttpContent Serialize<T>(T obj)
        {
            var test = JsonConvert.SerializeObject(obj, SerializerSettings);

            var stringContent = new StringContent(test);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue(HttpMimeType.Application.Json);

            return stringContent;
        }
        protected Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            return DeserializeAsync<T>(response, SerializerSettings);
        }
        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), settings);
        }
        protected Task<T> DeserializeUsingStreamAsync<T>(HttpResponseMessage response)
        {
            return DeserializeUsingStreamAsync<T>(response, SerializerSettings);
        }
        protected async Task<T> DeserializeUsingStreamAsync<T>(HttpResponseMessage response, JsonSerializerSettings settings)
        {
            var reader = JsonSerializer.Create(settings);
            using (var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                await using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return reader.Deserialize<T>(jsonTextReader);
                }
            }
        }
    }
}
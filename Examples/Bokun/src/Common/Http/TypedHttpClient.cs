using Common.Http.Exceptions;
using Common.Http.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Common.Http
{
	public class TypedHttpClient : HttpClient, ITypedHttpClient
	{
		private static readonly HttpMethod _patch = new HttpMethod("PATCH");


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
		public TypedHttpClient(JsonSerializerSettings serializerSettings, HttpMessageHandler handler, Boolean disposeHandler) : base(handler, disposeHandler)
		{
			SerializerSettings = serializerSettings;
		}


		// PROPERTIES /////////////////////////////////////////////////////////////////////////////
		public JsonSerializerSettings SerializerSettings { get; set; }


		// ITypedHttpClient ///////////////////////////////////////////////////////////////////////
		public async Task<T> GetObjectAsync<T>(String uri)
		{
			using(var response = await GetAsync(uri))
			{
				if(!response.IsSuccessStatusCode)
					throw new HttpServerException(response.StatusCode, $"Downstream {response.StatusCode}: GET {uri}");

				return await DeserializeUsingStreamAsync<T>(response, SerializerSettings);
			}
		}
		public async Task<T> GetObjectAsync<T>(String uri, T schema)
		{
			using(var response = await GetAsync(uri))
			{
				if(response.IsSuccessStatusCode)
					return JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), schema);

				throw new HttpServerException(response.StatusCode, $"Downstream {(Int32)response.StatusCode}: GET {uri}");
			}
		}
		public async Task<HttpResponseMessage> PostObjectAsync<T>(String uri, T obj)
		{
			var content = obj as HttpContent;
			if(content != null)
				return await PostAsync(uri, content);

			return await PostAsync(uri, Serialize(obj, SerializerSettings));
		}
		public async Task<HttpResponseMessage> PutObjectAsync<T>(String uri, T obj)
		{
			var content = obj as HttpContent;
			if(content != null)
				return await PutAsync(uri, content);

			return await PutAsync(uri, Serialize(obj, SerializerSettings));
		}
		public async Task<HttpResponseMessage> PatchObjectAsync<T>(String uri, JsonPatchDocument<T> obj) where T : class
		{
			return await PatchAsync(uri, Serialize(obj, SerializerSettings));
		}

		Task<HttpResponseMessage> ITypedHttpClient.PatchAsync(String requestUri, HttpContent content)
		{
			return SendAsync(new HttpRequestMessage(_patch, requestUri)
			{
				Content = content
			});
		}
		Task<HttpResponseMessage> ITypedHttpClient.GetAsync(String requestUri)
		{
			return GetAsync(requestUri);
		}
		Task<HttpResponseMessage> ITypedHttpClient.PostAsync(String requestUri, HttpContent content)
		{
			return PostAsync(requestUri, content);
		}
		Task<HttpResponseMessage> ITypedHttpClient.PutAsync(String requestUri, HttpContent content)
		{
			return PutAsync(requestUri, content);
		}
		Task<HttpResponseMessage> ITypedHttpClient.DeleteAsync(String requestUri)
		{
			return DeleteAsync(requestUri);
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		protected String Serialize<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj, SerializerSettings);
		}
		protected HttpContent Serialize<T>(T obj, JsonSerializerSettings settings)
		{
			var stringContent = new StringContent(JsonConvert.SerializeObject(obj, settings));
			stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return stringContent;
		}
		protected T Deserialize<T>(String response)
		{
			return JsonConvert.DeserializeObject<T>(response, SerializerSettings);
		}
		protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), settings);
		}
		protected async Task<T> DeserializeUsingStreamAsync<T>(HttpResponseMessage response, JsonSerializerSettings settings)
		{
			var reader = JsonSerializer.Create(settings);
			using(var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
			{
				using(var jsonTextReader = new JsonTextReader(streamReader))
				{
					return reader.Deserialize<T>(jsonTextReader);
				}
			}
		}
	}
}
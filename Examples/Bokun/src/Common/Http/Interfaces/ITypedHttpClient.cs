using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Http.Interfaces
{
	public interface ITypedHttpClient : IDisposable
	{
		Task<T> GetObjectAsync<T>(String uri);
		Task<T> GetObjectAsync<T>(String uri, T schema);
		Task<HttpResponseMessage> PostObjectAsync<T>(String uri, T obj);
		Task<HttpResponseMessage> PutObjectAsync<T>(String uri, T obj);
		Task<HttpResponseMessage> PatchObjectAsync<T>(String uri, JsonPatchDocument<T> obj) where T : class;
		Task<HttpResponseMessage> GetAsync(String requestUri);
		Task<HttpResponseMessage> PostAsync(String requestUri, HttpContent content);
		Task<HttpResponseMessage> PutAsync(String requestUri, HttpContent content);
		Task<HttpResponseMessage> PatchAsync(String requestUri, HttpContent content);
		Task<HttpResponseMessage> DeleteAsync(String requestUri);
	}
}
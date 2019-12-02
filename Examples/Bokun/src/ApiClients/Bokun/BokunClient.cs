using ApiClients.Bokun.Interfaces;
using ApiClients.StackDriver.Models.Config;
using Common.Consts;
using Common.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiClients.Bokun
{
	public class BokunClient : TypedHttpClient, IBokunClient
	{
		private readonly BokunClientConfig _config;


		public BokunClient(BokunClientConfig config)
		{
			_config = config;
			SerializerSettings = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			BaseAddress = new Uri(_config.BaseUrl);

			DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpMimeType.Application.Json));
		}


		// IBokunClient ///////////////////////////////////////////////////////////////////////////

		// Country
		public async Task<String> CurrencyFindAllAsync()
		{
			var timeStampStr = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

			//var timeStampStr = "2019-10-29 07:43:51";
			const String url = "/currency.json/findAll";
			var signatureStr = $"{timeStampStr}{_config.AccessKey}GET{url}";

			var signature = CalculateSha1(signatureStr);

			using(var request = new HttpRequestMessage()
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"{_config.BaseUrl}{url}")
			})
			{
				request.Headers.Add("X-Bokun-Signature", signature);
				request.Headers.Add("X-Bokun-AccessKey", _config.AccessKey);
				request.Headers.Add("X-Bokun-Date", timeStampStr);

				using(var response = await SendAsync(request))
				{
					if(response.IsSuccessStatusCode)
						return await response.Content.ReadAsStringAsync();

					throw new Exception(await response.Content.ReadAsStringAsync());
				}
			}
		}

		// Activity
		public async Task<String> ActivitySearchAsync()
		{
			//var timeStampStr = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

			var timeStampStr = "2013-11-09 14:33:46";
			const String url = "/activity.json/search?lang=EN&currency=ISK";
			var signatureStr = $"{timeStampStr}{_config.AccessKey}POST{url}";

			var signature = CalculateSha1(signatureStr);

			using(var request = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri($"{_config.BaseUrl}{url}"),
				Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}", Encoding.UTF8)
			})
			{
				request.Headers.Add("X-Bokun-Signature", signature);
				request.Headers.Add("X-Bokun-AccessKey", _config.AccessKey);
				request.Headers.Add("X-Bokun-Date", timeStampStr);

				using(var response = await SendAsync(request))
				{
					if(response.IsSuccessStatusCode)
						return await response.Content.ReadAsStringAsync();

					throw new Exception(await response.Content.ReadAsStringAsync());
				}
			}
		}

		// SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
		private static String CalculateSha1(String str)
		{
			using(var sha1 = SHA1.Create())
			{
				return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(str)));
			}
		}
	}
}
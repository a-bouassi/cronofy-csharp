using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Cronofy.Responses;
using Cronofy.Requests;
using Cronofy;

namespace Cronofy
{
	public sealed class CronofyAccountClient
	{
		private const string ReadEventsUrl = "https://api.cronofy.com/v1/events";
		private const string UpsertEventUrlFormat = "https://api.cronofy.com/v1/calendars/{0}/events";
		
		private readonly string accessToken;

		public CronofyAccountClient(string accessToken)
		{
			this.accessToken = accessToken;
			this.HttpClient = new ConcreteHttpClient();
		}

		/// <summary>
		/// Gets or sets the HTTP client.
		/// </summary>
		/// <value>
		/// The HTTP client.
		/// </value>
		/// <remarks>
		/// Intend for test purposes only.
		/// </remarks>
		internal IHttpClient HttpClient { get; set; }

		public IEnumerable<Event> GetEvents()
		{
			var request = new HttpRequest();

			request.Method = "GET";
			request.Url = ReadEventsUrl;
			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
			};
			request.QueryString = new Dictionary<string, string> {
				{ "tzid", "Etc/UTC" },
				{ "localized_times", "true" },
			};

			var response = HttpClient.GetResponse(request);
			var readEventsResponse = JsonConvert.DeserializeObject<ReadEventsResponse>(response.Body, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

			// TODO Support parameters
			// TODO Support pages

			return readEventsResponse.Events.Select(e => e.ToEvent());
		}

		public void UpsertEvent(string calendarId, UpsertEventRequestBuilder builder)
		{
			var request = builder.Build();
			UpsertEvent(calendarId, request);
		}

		public void UpsertEvent(string calendarId, UpsertEventRequest eventRequest)
		{
			var request = new HttpRequest();

			request.Method = "POST";
			request.Url = string.Format(UpsertEventUrlFormat, calendarId);
			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
				{ "Content-Type", "application/json; charset=utf-8" },
			};

			request.Body = JsonConvert.SerializeObject(eventRequest);

			var response = HttpClient.GetResponse(request);

			if (response.Code != 202) {
				// TODO More useful exceptions
				throw new ApplicationException("Request failed");
			}
		}
	}
}

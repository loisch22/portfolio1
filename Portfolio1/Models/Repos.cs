using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Portfolio.Models
{
    public class Repos
    {
		public int id { get; set; }
		public string name { get; set; }
		public string full_name { get; set; }

        public static List<Repos> GetRepos()
        {
			var client = new RestClient();
			client.BaseUrl = new Uri("https://api.github.com");

			var request = new RestRequest("search/repositories?q=user:loisch22&stars:>=1", Method.GET);
			request.AddHeader("User-Agent", "loisch22");
			request.AddParameter("per_page", "3");
			request.AddParameter("direction", "desc");

			var response = new RestResponse();

			Task.Run(async () =>
			{
				response = await GetResponseContentAsync(client, request) as RestResponse;
			}).Wait();

			JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
			var repoList = JsonConvert.DeserializeObject<List<Repos>>(jsonResponse["items"].ToString());

            return repoList;
		}

		public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
		{
			var tcs = new TaskCompletionSource<IRestResponse>();
			theClient.ExecuteAsync(theRequest, response => {
				tcs.SetResult(response);
			});
			return tcs.Task;
		}
    }
}

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace LogSearch
{
    class Program
    {

        class LogSearchQuery
        {
            public string Query { get; set; }
            public int Top { get; set; }

            public DateTime Start { get; set; }

            public DateTime End { get; set; }
        }
        static void Main(string[] args)
        {
            var client = new Program();
            client.ExecAsync().Wait();
        }

        async Task ExecAsync()
        {

            string result = await SearchAsync("Type=CQRTelemetry_CL");
            Console.WriteLine($"Search Key: *\n");
            // Console.WriteLine(result);
            Console.ReadLine();
        }

        public async Task<string> SearchAsync(string query)
        {
            // Authorization header for the Azure REST API
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetAccessToken());
            // Content with Content-Type
            var content = new StringContent(GetQueryString(query), Encoding.UTF8, "application/json");
            // Execute
            var response = await httpClient.PostAsync(GetUri(), content);

            // Deserialize the Json into Model objects
            var resultObject = JsonConvert.DeserializeObject<Rootobject>(await response.Content.ReadAsStringAsync());

            Console.WriteLine(resultObject.__metadata.resultType); // error or not.
            // Error Handling
            if (resultObject.__metadata.resultType == "error")
            {
                throw new Exception("Search Result Error!");
            }

            var values = resultObject.value != null ? resultObject.value : new Value[] { };

            foreach (var teremetry in values)
            {
                Console.WriteLine(teremetry.WindowStart_t);
            }
            return "OK";
        }

        private string GetQueryString(string query)
        {

            var queryJson = new LogSearchQuery()
            {
                Query = query,
                Top = 100,
                Start = new DateTime(2017, 7, 10),
                End = DateTime.UtcNow
            };

            return JsonConvert.SerializeObject(queryJson,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

        }

        private async Task<string> GetAccessToken()
        {
            var authority = String.Format(CultureInfo.InvariantCulture, AuthenticationEndpoint, tenantId);


            var authContext = new AuthenticationContext(authority);
            var creadential = new ClientCredential(clientId, clientSecret);
            var result = await authContext.AcquireTokenAsync("https://management.core.windows.net/", creadential);
            return result.AccessToken;

        }

        private string GetUri()
        {
            return $"{AzureURIBase}/subscriptions/{this.subscriptionId}/resourceGroups/{this.resourceGroup}/providers/Microsoft.OperationalInsights/workspaces/{this.workspaceName}/search?api-version=2015-03-20";
        }

        private string resourceGroup = ConfigurationManager.AppSettings["resourceGroup"];
        private string subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];
        private string clientId = ConfigurationManager.AppSettings["clientId"];
        private string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        private string tenantId = ConfigurationManager.AppSettings["tenantId"];
        private const string AuthenticationEndpoint = "https://login.windows.net/{0}";
        private const string RestAPIResource = "https://management.core.windows.net/";
        private const string AzureURIBase = "https://management.azure.com";
        private string workspaceName = "TelemetrySpike";
    }

}

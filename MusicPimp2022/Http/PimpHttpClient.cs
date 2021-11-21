using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Http
{
    class PimpHttpClient: IDisposable
    {
        static readonly DefaultContractResolver contractResolver = new()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        readonly JsonSerializerSettings settings = new()
        {
            ContractResolver = contractResolver
        };

        private readonly HttpClient client = new();
        private readonly string baseUrl = "https://cloud.musicpimp.org";

        public PimpHttpClient(Credential creds)
        {
            var headers = client.DefaultRequestHeaders;
            headers.Accept.ParseAdd("application/vnd.musicpimp.v18+json");
            var value = HttpUtil.Base64ColonSeparated(creds.Server, creds.Username, creds.Password);
            headers.Authorization = new AuthenticationHeaderValue("Basic", value);
        }

        public Task<VersionResponse> PingAuth()
        {
            return GetAsync<VersionResponse>("/pingauth");
        }

        public Task<HealthResponse> Health()
        {
            return GetAsync<HealthResponse>("/health");
        }

        public Task<LibraryResponse> Folder(string id)
        {
            return GetAsync<LibraryResponse>($"/folders/{id}");
        }

        private async Task<T> GetAsync<T>(string path)
        {
            var url = $"{baseUrl}{path}";
            try
            {
                var res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    Debug.Print($"Status is {res.StatusCode}");
                    var str = await res.Content.ReadAsStringAsync();
                    Debug.Print($"Got string {str}");
                    var t = JsonConvert.DeserializeObject<T>(str, settings);
                    Debug.Print($"Status: {res.StatusCode} content: {str}");
                    return t;
                }
                else
                {
                    var str = await res.Content.ReadAsStringAsync();
                    Debug.Print($"Request failed {str}");
                    var t = JsonConvert.DeserializeObject<ReasonResponse>(str, settings);
                    return await Task.FromException<T>(new HttpException(url, res, t.Reason));
                }
            }
            catch (HttpException he)
            {
                return await Task.FromException<T>(he);
            }
            catch (Exception e)
            {
                Debug.Print($"Exception in GetAsync {e}");
                return await Task.FromException<T>(e);
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}

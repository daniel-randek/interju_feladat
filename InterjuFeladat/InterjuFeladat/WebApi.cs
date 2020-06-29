using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterjuFeladat
{
    public class WebApi
    {
        private string url { get; set; }
        public WebApi(string url)
        {
            this.url = url;
        }

        public async Task<List<Country>> GetCountries()
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url);
            string json = await response;
            var data = JsonSerializer.Deserialize<List<Country>>(await response);
            return data;
        }
    }
}

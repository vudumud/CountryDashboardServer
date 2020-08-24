using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CountryDashboardServer.Shared
{
    public static class ApiHelper
    {
        public static HttpClient client { get; set; } = new HttpClient();

        public static void InitializeClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }       
    }
}

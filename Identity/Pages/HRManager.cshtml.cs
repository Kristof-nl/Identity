using Identity.Authorization;
using Identity.DTO;
using Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Identity.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> WeatherForecastItems { get; set; }

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            WeatherForecastItems = await InvokeEndPoint<List<WeatherForecastDTO>>("OurWebAPI", "WeatherForecast");
        }

        private async Task<T> InvokeEndPoint<T>(string clientName, string url)
        {
            //get token from session
            JwtToken token = null;

            var strTokenObj = HttpContext.Session.GetString("acces_token");
            if (string.IsNullOrEmpty(strTokenObj))
            {
                token = await Authenticate();
            }
            else
                token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj);

            if (token == null ||
                string.IsNullOrWhiteSpace(token.AccessToken) ||
                token.ExpiredAt <= DateTime.UtcNow)
                token = await Authenticate();


            var httpClient = httpClientFactory.CreateClient(clientName);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            return await httpClient.GetFromJsonAsync<T>(url);
        }

        private async Task <JwtToken> Authenticate()
        {
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");
            var res = await httpClient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "password" });
            res.EnsureSuccessStatusCode();
            string strJwt = await res.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("acces_token", strJwt);

            return JsonConvert.DeserializeObject<JwtToken>(strJwt);
        }
    }
}
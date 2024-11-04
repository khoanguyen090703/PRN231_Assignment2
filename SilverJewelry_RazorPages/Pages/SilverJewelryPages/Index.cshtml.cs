using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SilverJewelry_BOs;
using SilverJewelry_DAO.Data;

namespace SilverJewelry_RazorPages.Pages.SilverJewelryPages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel()
        {
            _httpClient = new HttpClient();
        }

        public IList<SilverJewelry> SilverJewelry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if(string.IsNullOrEmpty(accessToken))
            {
                RedirectToPage("/Login");
            }

            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5174/SilverJewelry");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            SilverJewelry = JsonSerializer.Deserialize<List<SilverJewelry>>(data, options);
        }
    }
}

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
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel()
        {
            _httpClient = new HttpClient();
        }

        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                RedirectToPage("/Login");
            }

            //var role = HttpContext.Session.GetInt32("Role");
            //if (role == null || role != 1)
            //{
            //    RedirectToPage("/Privacy");
            //}

            if (id == null)
            {
                return NotFound();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5174/SilverJewelry/" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var data = await response.Content.ReadAsStringAsync();
                var silverJewelry = JsonSerializer.Deserialize<SilverJewelry>(data, options);

                if (silverJewelry == null)
                {
                    return NotFound();
                }
                else
                {
                    SilverJewelry = silverJewelry;
                }
                return Page();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/Privacy");
            }
            return RedirectToPage("./Index");
        }
    }
}

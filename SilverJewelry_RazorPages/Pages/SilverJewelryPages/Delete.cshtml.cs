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
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel()
        {
           _httpClient = new HttpClient();
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login");
            }

            var role = HttpContext.Session.GetInt32("Role");
            if (role == null || role != 1)
            {
                return RedirectToPage("/Privacy");
            }

            if (id == null)
            {
                return NotFound();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5174/SilverJewelry/" + id);
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
            SilverJewelry = silverJewelry;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.DeleteAsync("http://localhost:5174/SilverJewelry/" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("./Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/Privacy", new { message = "You are not allowed to access delete function!" });
            }

            return RedirectToPage();
        }
    }
}

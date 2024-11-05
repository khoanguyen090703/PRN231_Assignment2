using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverJewelry_BOs;
using SilverJewelry_DAO.Data;

namespace SilverJewelry_RazorPages.Pages.SilverJewelryPages
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel()
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

            HttpResponseMessage categoriesResponse = await _httpClient.GetAsync("http://localhost:5174/Category");
            var categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            var accessToken = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpContent content = JsonContent.Create(SilverJewelry);
            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5174/SilverJewelry/" + SilverJewelry.SilverJewelryId, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("./Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/Privacy", new {message = "You are not allowed to access edit function!"});
            }

            return RedirectToPage();
        }
    }
}

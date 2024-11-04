using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SilverJewelry_BOs;
using SilverJewelry_DAO.Data;

namespace SilverJewelry_RazorPages.Pages.SilverJewelryPages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> OnGet()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5174/Category");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<Category>>(data, options);

            ViewData["Category"] = new SelectList(categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpContent content = JsonContent.Create(SilverJewelry);
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5174/SilverJewelry", content);

            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToPage("./Index");
            }

            return RedirectToPage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SilverJewelry_BOs;
using SilverJewelry_DAO.Data;
using SilverJewelry_RazorPages.Models.Request;
using SilverJewelry_RazorPages.Models.Response;

namespace SilverJewelry_RazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpContent content = JsonContent.Create(LoginRequest);
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5174/BranchAccount/login", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(data, options);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.AccessToken))
                {
                    HttpContext.Session.SetString("AccessToken", loginResponse.AccessToken);
                    HttpContext.Session.SetInt32("Role", loginResponse.Role);
                    return RedirectToPage("/SilverJewelryPages/Index");
                }
            }

            return RedirectToPage();
        }
    }
}

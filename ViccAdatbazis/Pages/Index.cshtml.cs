using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<JokeDto> Jokes { get; set; }
        public int CurrentPage { get; set; } = 1;

        public async Task OnGetAsync(int page = 1)
        {
            CurrentPage = page;
            var response = await _httpClient.GetAsync($"/api/jokes?page={page}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Jokes = JsonSerializer.Deserialize<List<JokeDto>>(jsonResponse);
            }
        }

        public async Task<IActionResult> OnPostLikeAsync(int id)
        {
            await _httpClient.PatchAsync($"/api/jokes/{id}/like", null);
            return RedirectToPage(new { page = CurrentPage });
        }

        public async Task<IActionResult> OnPostDislikeAsync(int id)
        {
            await _httpClient.PatchAsync($"/api/jokes/{id}/dislike", null);
            return RedirectToPage(new { page = CurrentPage });
        }
        public class JokeDto
        {
            public int Id { get; set; }
            public string Tartalom { get; set; }
            public string Feltolto { get; set; }
            public int Tetszik { get; set; }
            public int NemTetszik { get; set; }
        }
    }
}
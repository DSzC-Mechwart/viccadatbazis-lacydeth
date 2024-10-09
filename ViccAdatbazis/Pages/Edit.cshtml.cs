using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ViccAdatbazis.Models;
using System.Text.Json;

public class EditModel : PageModel
{
    private readonly HttpClient _httpClient;

    public EditModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public JokeDto Joke { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/jokes/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Joke = JsonSerializer.Deserialize<JokeDto>(jsonResponse);
            return Page();
        }
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/jokes/{Joke.Id}", Joke);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Jokes/Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostArchiveAsync(int id)
    {
        await _httpClient.DeleteAsync($"/api/jokes/{id}");
        return RedirectToPage("/Jokes/Index");
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

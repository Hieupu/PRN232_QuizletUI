using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN232_QuizletUI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PRN232_QuizletUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _api;

        public IndexModel(ApiService api)
        {
            _api = api;
        }

        public List<FlashcardSetViewModel> FlashcardSets { get; set; } = new();

        public int Page { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string? search = "", int page = 1)
        {
            string apiUrl = $"https://localhost:7225/api/flashcardsets?page={page}&pageSize=8";

            if (!string.IsNullOrWhiteSpace(search))
                apiUrl += $"&search={Uri.EscapeDataString(search)}";

            var response = await _api.GetAsync<FlashcardSetResponse>(apiUrl);

            FlashcardSets = response?.Data ?? new List<FlashcardSetViewModel>();
            Page = response?.Page ?? 1;
            TotalPages = response?.TotalPages ?? 1;
        }


        public class FlashcardSetViewModel
        {
            public int SetId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
            public int StudyCount { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class FlashcardSetResponse
        {
            public int Total { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
            public List<FlashcardSetViewModel> Data { get; set; } = new();
        }
    }
}

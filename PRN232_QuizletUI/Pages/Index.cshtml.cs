using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN232_QuizletUI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            FlashcardSets = await _api.GetAsync<List<FlashcardSetViewModel>>("https://localhost:7225/api/flashcardsets")
                             ?? new List<FlashcardSetViewModel>();
        }

        public class FlashcardSetViewModel
        {
            public int SetID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public int StudyCount { get; set; }
        }
    }
}

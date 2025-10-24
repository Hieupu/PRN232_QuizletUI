using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN232_QuizletUI.Services;

namespace PRN232_QuizletUI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApiService _api;
        public LoginModel(ApiService api) => _api = api;

        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";
        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var data = new { Email, Password };
            var result = await _api.PostAsync<dynamic>("https://localhost:7225/api/users/login", data);

            if (result != null)
            {
                Message = "Login success!";
            }
            else
            {
                Message = "Login failed.";
            }
            return Page();
        }
    }
}

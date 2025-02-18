using Infrastructure.Entity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;
using RecipeApplication.Pages.Recipes;

namespace RecipeApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ICollection<RecipeSummaryViewModel> Recipes { get; set; }
        private readonly RecipeService _service;
        public IndexModel(ILogger<IndexModel> logger, RecipeService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Recipes = await _service.GetRecipes();

            if (Recipes is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;

namespace RecipeApplication.Pages.Recipes
{
    public class ViewModel : PageModel
    {
        public RecipeDetailViewModel Recipe { get; set; }
        private readonly RecipeService _service;
        public ViewModel(RecipeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _service.GetRecipeDetail(id);
            if (Recipe is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

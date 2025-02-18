using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;

namespace RecipeApplication.Pages.Recipes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UpdateRecipeCommand Input { get; set; }
        private readonly RecipeService _recipeService;
        public EditModel(RecipeService recipeService)
        {
            _recipeService= recipeService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            Input = await _recipeService.GetRecipeForUpdate(id);

            if (Input == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _recipeService.UpdateRecipe(Input);
                    return RedirectToPage("View", new { id = Input.Id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Page();
        }
    }
}

using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApplication.Models;

namespace RecipeApplication.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRecipeCommand Input { get; set; }
        private readonly RecipeService _service;
        public CreateModel(RecipeService service)
        {
            _service= service;
        }
        public void OnGet()
        {
            Input = new CreateRecipeCommand();
        }

        public async Task<IActionResult> OnPost()
        {
            try
             {
                if  (ModelState.IsValid)
                {
                    var id = await _service.CreateRecipe(Input);
                    return RedirectToPage("View", new { id });
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

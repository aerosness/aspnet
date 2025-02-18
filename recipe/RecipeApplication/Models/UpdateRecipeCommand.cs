using Infrastructure.Entity;

namespace RecipeApplication.Models
{
    public class UpdateRecipeCommand : EditRecipeBase
    {
        public int Id { get; set; }

        public void UpdateRecipe(Recipe recipe, UpdateRecipeCommand cmd)
        {
            recipe.Name = cmd.Name;
            recipe.Method = cmd.Method;
            recipe.TimeToCook = new TimeSpan(cmd.TimeToCookHrs, cmd.TimeToCookMins, 0);
            recipe.IsVegetirian = cmd.IsVegeterian;
            recipe.IsVegan = cmd.IsVegan;
        }
    }
}

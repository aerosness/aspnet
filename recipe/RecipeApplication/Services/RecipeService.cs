using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using RecipeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RecipeService
    {
        readonly AppDbContext _context;
        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRecipe(CreateRecipeCommand cmd)
        {
            var recipe = new Recipe
            {
                Name = cmd.Name,
                TimeToCook = new TimeSpan(
                    cmd.TimeToCookHrs, cmd.TimeToCookMins, 0),
                Method = cmd.Method,
                IsVegetirian = cmd.IsVegeterian,
                IsVegan = cmd.IsVegan,
                Ingredients = cmd.Ingredients?
                .Select(i =>
                new Ingredient
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList()
            };
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe.RecipeId;
        }

        public async Task<ICollection<RecipeSummaryViewModel>> GetRecipes()
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted)
                .Select(r => new RecipeSummaryViewModel
                {
                    Id = r.RecipeId,
                    Name = r.Name,
                    TimeToCook = $"{r.TimeToCook.TotalMinutes}mins"
                })
                .ToListAsync();
        }

        public async Task<RecipeDetailViewModel> GetRecipeDetail(int id)
        {
            return await _context.Recipes
                .Where(r => r.RecipeId == id)
                .Select(r => new RecipeDetailViewModel
                {
                    Id = r.RecipeId,
                    Name = r.Name,
                    Method = r.Method,
                    Ingregients = r.Ingredients
                    .Select(i => new RecipeDetailViewModel.Item
                    {
                        Name = i.Name,
                        Quantity = $"{i.Quantity} {i.Unit}"
                    })
                })
                .SingleOrDefaultAsync();
        }

        public async Task UpdateRecipe(UpdateRecipeCommand cmd)
        {
            var recipe = await _context.Recipes.FindAsync(cmd.Id);
            if (recipe == null)
            {
                throw new Exception("Unable to find the recipe");
            }
            cmd.UpdateRecipe(recipe, cmd);
            await _context.SaveChangesAsync();
        }

        public async Task<UpdateRecipeCommand> GetRecipeForUpdate(int id)
        {
            return await _context.Recipes
                .Where(r => r.RecipeId == id)
                .Select(r => new UpdateRecipeCommand
                {
                    Id = r.RecipeId,
                    Name = r.Name,
                    Method = r.Method,
                    TimeToCookHrs = r.TimeToCook.Hours,
                    TimeToCookMins = r.TimeToCook.Minutes,
                    IsVegeterian = r.IsVegetirian,
                    IsVegan = r.IsVegan
                })
                .SingleOrDefaultAsync();
        }
    }
}

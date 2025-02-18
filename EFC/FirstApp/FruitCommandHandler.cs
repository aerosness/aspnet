using FirstApp.Model;
using FirstApp.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FirstApp
{
    public class FruitCommandHandler : ICommand
    {
        private readonly AppDbContext _context;

        public FruitCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public IResult GetById(int fruitId)
        {
            var fruit = _context.Fruits.Find(fruitId);
            if (fruit == null)
            {
                return Results.Problem(detail: "Фрукт не найден", statusCode: 404);
            }
            return TypedResults.Ok(fruit);
        }

        public IResult CreateFruit(int fruitId, Fruit newFruit)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newFruit);

            if (!Validator.TryValidateObject(newFruit, validationContext, validationResults, true))
            {
                var validationErrors = validationResults.ToDictionary(
                    v => v.MemberNames.FirstOrDefault() ?? "Ошибка",
                    v => new[] { v.ErrorMessage ?? "Некорректные данные" }
                );
                return Results.ValidationProblem(validationErrors);
            }

            _context.Fruits.Add(newFruit);
            _context.SaveChanges();
            return TypedResults.Created($"/fruit/{fruitId}", newFruit);
        }

        public IResult UpdateFruit(int fruitId, Fruit updatedFruit)
        {
            var existingFruit = _context.Fruits.Find(fruitId);
            if (existingFruit == null)
            {
                return Results.Problem(detail: "Фрукт не найден", statusCode: 404);
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updatedFruit);

            if (!Validator.TryValidateObject(updatedFruit, validationContext, validationResults, true))
            {
                var validationErrors = validationResults.ToDictionary(
                    v => v.MemberNames.FirstOrDefault() ?? "Ошибка",
                    v => new[] { v.ErrorMessage ?? "Некорректные данные" }
                );
                return Results.ValidationProblem(validationErrors);
            }

            existingFruit.Name = updatedFruit.Name;
            existingFruit.Store = updatedFruit.Store;
            _context.SaveChanges();
            return TypedResults.Ok(existingFruit);
        }

        public IResult DeleteFruit(int fruitId)
        {
            var fruit = _context.Fruits.Find(fruitId);
            if (fruit == null)
            {
                return Results.Problem(detail: "Фрукт не найден", statusCode: 404);
            }

            _context.Fruits.Remove(fruit);
            _context.SaveChanges();
            return Results.NoContent();
        }

        public IResult GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

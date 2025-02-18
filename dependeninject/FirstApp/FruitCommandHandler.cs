using FirstApp.Model;
using System.ComponentModel.DataAnnotations;

namespace FirstApp
{
    public static class FruitData
    {
        public static Dictionary<int, Fruit> Fruits = new Dictionary<int, Fruit>();
    }

    public class FruitCommandHandler : ICommand
    {
        public IResult GetAll()
        {
            return TypedResults.Ok(FruitData.Fruits.Values);
        }

        public IResult GetById(int fruitId)
        {
            if (!FruitData.Fruits.TryGetValue(fruitId, out var fruit))
            {
                return Results.Problem(detail: "фрукт не найден", statusCode: 404);
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

            FruitData.Fruits[fruitId] = newFruit;
            return TypedResults.Created($"/fruit/{fruitId}", newFruit);
        }

        public IResult UpdateFruit(int fruitId, Fruit updatedFruit)
        {
            if (!FruitData.Fruits.ContainsKey(fruitId))
            {
                return Results.Problem(detail: "фрукт не найден", statusCode: 404);
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

            FruitData.Fruits[fruitId] = updatedFruit;
            return TypedResults.Ok(updatedFruit);
        }

        public IResult DeleteFruit(int fruitId)
        {
            if (!FruitData.Fruits.ContainsKey(fruitId))
            {
                return Results.Problem(detail: "фрукт не найден", statusCode: 404);
            }

            FruitData.Fruits.Remove(fruitId);
            return Results.NoContent();
        }
    }
}

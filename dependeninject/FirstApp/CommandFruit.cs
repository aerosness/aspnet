using FirstApp.Model;
using System.ComponentModel.DataAnnotations;

namespace FirstApp
{
    public static class Dannye
    {
        public static Dictionary<int, Fruit> vse = new Dictionary<int, Fruit>();
    }

    public class CommandFruit : ICommand
    {
        public IResult GetAll()
        {
            return TypedResults.Ok(Dannye.vse.Values);
        }
        
        public IResult GetById(int id)
        {
            if (!Dannye.vse.TryGetValue(id, out var fruktik))
            {
                return Results.Problem(detail: "Фрукт жок", statusCode: 404);
            }
            return TypedResults.Ok(fruktik);
        }

        public IResult CreateFruit(int id, Fruit fruktik)
        {
            var validatsia = new List<ValidationResult>();
            var kontekst = new ValidationContext(fruktik);
            
            if (!Validator.TryValidateObject(fruktik, kontekst, validatsia, true))
            {
                var oshibochki = validatsia.ToDictionary(
                    v => v.MemberNames.FirstOrDefault() ?? "Ошибка",
                    v => new[] { v.ErrorMessage ?? "Некорректна" }
                );
                return Results.ValidationProblem(oshibochki);
            }
            
            Dannye.vse[id] = fruktik;
            return TypedResults.Created($"/fruit/{id}", fruktik);
        }

        public IResult UpdateFruit(int id, Fruit fruktik)
        {
            if (!Dannye.vse.ContainsKey(id))
            {
                return Results.Problem(detail: "Фрукт жок", statusCode: 404);
            }
            
            var validatsia = new List<ValidationResult>();
            var kontekst = new ValidationContext(fruktik);
            
            if (!Validator.TryValidateObject(fruktik, kontekst, validatsia, true))
            {
                var oshibochki = validatsia.ToDictionary(
                    v => v.MemberNames.FirstOrDefault() ?? "Ошибка",
                    v => new[] { v.ErrorMessage ?? "Некорректна" }
                );
                return Results.ValidationProblem(oshibochki);
            }
            
            Dannye.vse[id] = fruktik;
            return TypedResults.Ok(fruktik);
        }

        public IResult DeleteFruit(int id)
        {
            if (!Dannye.vse.ContainsKey(id))
            {
                return Results.Problem(detail: "Фрукт жок", statusCode: 404);
            }
            
            Dannye.vse.Remove(id);
            return Results.NoContent();
        }
    }
}
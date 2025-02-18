using FirstApp.Model;
using FirstApp.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FirstApp
{
    public class CommandFruit : ICommand
    {
        private readonly AppDbContext _context;

        public CommandFruit(AppDbContext context)
        {
            _context = context;
        }

        public IResult GetById(int id)
        {
            var fruktik = _context.Fruits.Find(id);
            if (fruktik == null)
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
                    v => new[] { v.ErrorMessage ?? "Некорректное значение" }
                );
                return Results.ValidationProblem(oshibochki);
            }
            
            _context.Fruits.Add(fruktik);
            _context.SaveChanges();
            return TypedResults.Created($"/fruit/{id}", fruktik);
        }

        public IResult UpdateFruit(int id, Fruit fruktik)
        {
            var existingFruit = _context.Fruits.Find(id);
            if (existingFruit == null)
            {
                return Results.Problem(detail: "Фрукт жок", statusCode: 404);
            }
            
            var validatsia = new List<ValidationResult>();
            var kontekst = new ValidationContext(fruktik);
            
            if (!Validator.TryValidateObject(fruktik, kontekst, validatsia, true))
            {
                var oshibochki = validatsia.ToDictionary(
                    v => v.MemberNames.FirstOrDefault() ?? "Ошибка",
                    v => new[] { v.ErrorMessage ?? "Некорректное значение" }
                );
                return Results.ValidationProblem(oshibochki);
            }
            
            existingFruit.Name = fruktik.Name;
            existingFruit.Store = fruktik.Store;
            _context.SaveChanges();
            return TypedResults.Ok(existingFruit);
        }

        public IResult DeleteFruit(int id)
        {
            var fruktik = _context.Fruits.Find(id);
            if (fruktik == null)
            {
                return Results.Problem(detail: "Фрукт жок", statusCode: 404);
            }
            
            _context.Fruits.Remove(fruktik);
            _context.SaveChanges();
            return Results.NoContent();
        }
    }
}
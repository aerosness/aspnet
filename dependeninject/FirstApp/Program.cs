using FirstApp;
using FirstApp.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<ICommand, FruitCommandHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();

app.MapGet("/fruit/all", (ICommand fruitCommandHandler) => fruitCommandHandler.GetAll());
app.MapGet("/fruit/{id}", (int fruitId, ICommand fruitCommandHandler) => fruitCommandHandler.GetById(fruitId));
app.MapPost("/fruit/{id}", (int fruitId, [FromBody] Fruit newFruit, ICommand fruitCommandHandler) => fruitCommandHandler.CreateFruit(fruitId, newFruit));
app.MapPut("/fruit/{id}", (int fruitId, [FromBody] Fruit updatedFruit, ICommand fruitCommandHandler) => fruitCommandHandler.UpdateFruit(fruitId, updatedFruit));
app.MapDelete("/fruit/{id}", (int fruitId, ICommand fruitCommandHandler) => fruitCommandHandler.DeleteFruit(fruitId));

app.Run();

using FirstApp.Data;
using Microsoft.EntityFrameworkCore;
using FirstApp.Model;
using FirstApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=fruits.db"));

builder.Services.AddScoped<FruitCommandHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/fruit/{id}", (int id, FruitCommandHandler command) => command.GetById(id));
app.MapPost("/fruit/{id}", (int id, Fruit fruit, FruitCommandHandler command) => command.CreateFruit(id, fruit));
app.MapPut("/fruit/{id}", (int id, Fruit fruit, FruitCommandHandler command) => command.UpdateFruit(id, fruit));
app.MapDelete("/fruit/{id}", (int id, FruitCommandHandler command) => command.DeleteFruit(id));

app.Run();

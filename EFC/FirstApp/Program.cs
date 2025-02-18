using FirstApp.Data;
using Microsoft.EntityFrameworkCore;
using FirstApp.Model;
using FirstApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=fruits.db"));

builder.Services.AddScoped<CommandFruit>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/fruit/{id}", (int id, CommandFruit command) => command.GetById(id));
app.MapPost("/fruit/{id}", (int id, Fruit fruit, CommandFruit command) => command.CreateFruit(id, fruit));
app.MapPut("/fruit/{id}", (int id, Fruit fruit, CommandFruit command) => command.UpdateFruit(id, fruit));
app.MapDelete("/fruit/{id}", (int id, CommandFruit command) => command.DeleteFruit(id));

app.Run();

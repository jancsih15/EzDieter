using EzDieter.Database;
using EzDieter.Database.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database
//builder.Services.AddSingleton<MongoClientFactory>();
//builder.Services.AddScoped(provider => provider.GetRequiredService<MongoClientFactory>().Create());
builder.Services.AddScoped<IMongoClient>(provider => new MongoClient("mongodb://localhost:27017"));
builder.Services.AddScoped<IDailyRepository,DailyRepository>();
builder.Services.AddScoped<IIngredientRepository,IngredientRepository>();
builder.Services.AddScoped<IMealRepository,MealRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
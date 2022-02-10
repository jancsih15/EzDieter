using System;
using EzDieter.Api;
using EzDieter.Api.Helpers;
using EzDieter.Database;
using EzDieter.Database.Mongo;
using EzDieter.Domain;
using EzDieter.Logic;
using EzDieter.Logic.UserServices;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(x => x.FullName));



var dbname = builder.Configuration["database-name"];


// Database
builder.Services.AddSingleton<IMongoClient, MongoClient>(x =>
{
    var uri = x.GetRequiredService<IConfiguration>()["MongoUri"];
    return new MongoClient(uri);
});

// Repositories
builder.Services.AddScoped<IDailyRepository,DailyRepository>();
builder.Services.AddScoped<IIngredientRepository,IngredientRepository>();
builder.Services.AddScoped<IMealRepository,MealRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

// Helpers
builder.Services.AddScoped<IJwtUtils,JwtUtils>();

// Mediator (Services)
builder.Services.AddMediatR(typeof(GetUsersQuery));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares TODO add exception handler middleware 
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
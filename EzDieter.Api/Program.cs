using System;
using System.IO;
using EzDieter.Api;
using EzDieter.Api.Helpers;
using EzDieter.Database;
using EzDieter.Database.Mongo;
using EzDieter.Logic;
using EzDieter.Logic.UserServices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
System.Diagnostics.Process.Start(wantedPath + "\\EzDieter\\DockerCompose.bat");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(x => x.FullName));
//Delete
// builder.Services
//     .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
//     .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


//var dbname = builder.Configuration["database-name"];


// Database
builder.Services.AddSingleton<IMongoClient, MongoClient>(x =>
{
    var uri = x.GetRequiredService<IConfiguration>()["MongoUri"];
    return new MongoClient(uri);
});

// Repositories
builder.Services.AddScoped<IDayRepository,DayRepository>();
builder.Services.AddScoped<IIngredientRepository,IngredientRepository>();
builder.Services.AddScoped<IDishRepository,DishRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

// Helpers
builder.Services.AddScoped<IJwtUtils,JwtUtils>();
builder.Services.AddMediatR(typeof(GetUserByIdQuery));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
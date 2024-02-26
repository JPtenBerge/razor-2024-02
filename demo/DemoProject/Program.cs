using DemoProject.Entities;
using DemoProject.Repositories;
using DemoProject.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// globale instellingen
// dependency injection
// grote bouwblokken

builder.Services.AddTransient<IValidator<Character>, CharacterValidator>();
builder.Services.AddSingleton<ICharacterRepository, CharacterRepository>();
builder.Services.AddRazorPages().AddFluentValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
});

var app = builder.Build();

// dit is wat er ieder request moet gebeuren

app.UseStaticFiles(); // defaults to wwwroot/

app.MapRazorPages(); // Pages/

app.Run();

using DemoProject.Entities;
using DemoProject.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// globale instellingen
// dependency injection
// grote bouwblokken

builder.Services.AddScoped<IValidator<Character>, CharacterValidator>();

builder.Services.AddRazorPages().AddFluentValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
});

var app = builder.Build();

// dit is wat er ieder request moet gebeuren

app.MapRazorPages(); // Pages/

app.Run();

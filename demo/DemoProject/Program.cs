using DemoProject.DataAccess;
using DemoProject.Entities;
using DemoProject.Repositories;
using DemoProject.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// globale instellingen
// dependency injection
// grote bouwblokken

builder.Services.AddDbContext<AvatarContext>(options =>
{
    options.UseSqlServer("Server=.\\SQLEXPRESS; Database=avatardb; Integrated Security=true; TrustServerCertificate=True");
}, ServiceLifetime.Transient);

builder.Services.AddTransient<IValidator<Character>, CharacterValidator>();
// builder.Services.AddSingleton<ICharacterRepository, CharacterRepository>();
builder.Services.AddTransient<ICharacterRepository, CharacterDbRepository>();
builder.Services.AddRazorPages().AddFluentValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
});

var app = builder.Build();

// dit is wat er ieder request moet gebeuren

app.UseStaticFiles(); // defaults to wwwroot/

app.MapRazorPages(); // Pages/

app.Run();

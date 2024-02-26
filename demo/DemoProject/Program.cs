var builder = WebApplication.CreateBuilder(args);

// globale instellingen
// dependency injection
// grote bouwblokken

builder.Services.AddRazorPages();

var app = builder.Build();

// dit is wat er ieder request moet gebeuren

app.MapRazorPages(); // Pages/

app.Run();

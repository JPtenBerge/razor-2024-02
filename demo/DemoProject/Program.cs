using System.Reflection;
using System.Text.Json.Serialization;
using DemoProject.DataAccess;
using Demo.Shared.Dtos;
using Demo.Shared.Entities;
using DemoProject.Middleware;
using DemoProject.Repositories;
using Demo.Shared.Validators;
using DemoProject;
using DemoProject.Hubs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// globale instellingen
// dependency injection
// grote bouwblokken

builder.Services.AddIdentity<AvatarUser, IdentityRole>().AddEntityFrameworkStores<AvatarContext>();

builder.Services.AddTransient<MijnExceptionLoggingMiddleware>();
builder.Services.AddTransient<IValidator<CharacterPostRequestDto>, CharacterValidator>();
// builder.Services.AddSingleton<ICharacterRepository, CharacterRepository>();
builder.Services.AddTransient<ICharacterRepository, CharacterDbRepository>();
builder.Services.AddTransient<INationRepository, NationDbRepository>();
builder.Services.AddRazorPages().AddFluentValidation(options => { options.DisableDataAnnotationsValidation = true; });

builder.Services.AddCors(options =>
{
    options.AddPolicy("blazorfrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5080").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Mijn beste API aller tijden"
    });
});
builder.Services.AddMemoryCache();
// builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
//     .AddNewtonsoftJson(options =>
// {
//     // options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
// });

var connString = Environment.GetEnvironmentVariable("connstring");
connString ??= builder.Configuration.GetConnectionString("AvatarContext");
Console.WriteLine("Using connection string: " + connString);

builder.Services.AddDbContext<AvatarContext>(
    options =>
    {
        options.UseSqlServer(connString);
    }, ServiceLifetime.Transient);

var app = builder.Build();

// dit is wat er ieder request moet gebeuren

app
    .UseDeveloperExceptionPage()
    .UseMijnExceptionLoggingMiddleware() // extension method
    .UseStaticFiles(); // defaults to wwwroot/

app.UseCors("blazorfrontend");

app.UseAuthentication(); // leest auth cookie uit
app.UseAuthorization();

app.UseSwagger(); // docs

app.UseSession(); // session cookie uitlezen

if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(); // UI
}

app.MapHub<PollHub>("/pollHub");

app.MapControllers(); // Controllers/  [Route()]  : ControllerBase

app.MapRazorPages(); // Pages/

app.Run();
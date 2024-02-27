using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Infrastructure.Data;
using PhotoSharingApplication.Web;
using Microsoft.AspNetCore.Identity;
using PhotoSharingApplication.Web.Data;
using PhotoSharingApplication.Web.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Our own extension method, contained in the ServiceCollectionExtensions class
builder.Services
    .AddPhotoSharingServices()
    .AddPhotoSharingDb(builder.Configuration.GetConnectionString("Default"));
    
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PhotoSharingIdentityContext>();
builder.Services.AddDbContext<PhotoSharingIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoSharingIdentityContextConnection")));

//OpenApi Support
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//End OpenApi Support

builder.Services.AddAuthorization(options => {
    options.AddPolicy("PhotoDeletionPolicy", policy => {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new PhotoOwnerRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, PhotoOwnerAuthorizationHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    //OpenAPI support
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//minimal API
app.MapGet("/photos/image/{id:int}", async (int id, IPhotosService photosService) => {
    Photo? photo = await photosService.GetPhotoByIdAsync(id);
    if (photo is null || photo.PhotoFile is null) {
        return Results.NotFound();
    }
    return Results.File(photo.PhotoFile, photo.ContentType);
});
app.MapControllers();
app.MapRazorPages();

app.Run();

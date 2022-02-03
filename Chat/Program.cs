using AuthMiddlware.AuthMiddleware;
using AuthMiddlware.Extensions;
using AuthMiddlware.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Chat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(AuthScheme.DefaultScheme)
        .AddScheme<AuthOption, AuthHandler>(AuthScheme.DefaultScheme, null);
builder.Services.AddAuthorization();
builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromDays(20);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    }
);
builder.Services.AddDbContext<ChatDbContext>(options =>
                         options.UseSqlite(builder.Configuration.GetConnectionString("TestConnection")));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuth();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html"); ;

app.Run();

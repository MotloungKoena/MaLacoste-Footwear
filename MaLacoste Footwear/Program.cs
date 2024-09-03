using MaLacoste_Footwear.Data;
using MaLacoste_Footwear.Infrastructure;
using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();


//Database Option 1: SQL Server 
builder.Services.AddDbContext<AppDbContext>(options => options
        .UseSqlServer(builder.Configuration
        .GetConnectionString("MaLacosteConnection")));

builder.Services.AddDbContext<SneakerIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MaLacosteIdentityConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.User.RequireUniqueEmail = true;
    //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
}).AddEntityFrameworkStores<SneakerIdentityDbContext>();

builder.Services.AddScoped<IPasswordValidator<AppUser>, CustomPasswordValidator>();
builder.Services.AddScoped<IUserValidator<AppUser>, CustomUserValidator>();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// route for sorting and paging category
app.MapControllerRoute(
        name: "sortingpagingcategory",
        pattern: "{controller}/{action}/{id}/orderby{sortBy}/page{bookPage}");

// route for paging all books
app.MapControllerRoute(
    name: "allpaging",
    pattern: "{controller}/{action}/{id=all}/page{bookPage}");

// route for sorting
app.MapControllerRoute(
    name: "sortingcategory",
    pattern: "{controller}/{action}/{id}/orderby{sortBy}");

// least specific route - 0 required segments 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");


SeedData.EnsurePopulated(app);
//SeedData.EnsurePopulated(app);
SeedDataIdentity.EnsurePopulated(app);

app.Run();
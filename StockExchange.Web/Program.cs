using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockExchange.Application.Abstraction;
using StockExchange.Application.Implementation;
using StockExchange.Infrastructure.Database;
using StockExchange.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("MySQL");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<StockExchangeDbContext>(
    options => options.UseMySql(connectionString, serverVersion)
);

//Configuration for Identity
builder.Services.AddIdentity<User, Role>()
     .AddEntityFrameworkStores<StockExchangeDbContext>()
     .AddDefaultTokenProviders();

builder.Services.AddScoped<IStockAppService, StockAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
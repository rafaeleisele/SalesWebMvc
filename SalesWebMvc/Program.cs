using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesRecordService>();

var app = builder.Build();

////Alterando a localização da aplicação
//var enUS = new CultureInfo("en-US");
//var LocalizationOptions = new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(enUS),
//    SupportedCultures = new List<CultureInfo> { enUS },
//    SupportedUICultures = new List<CultureInfo> { enUS }
//};

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Departments}/{action=Index}/{id?}");

app.Run();

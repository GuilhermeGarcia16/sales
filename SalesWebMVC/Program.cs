using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using SalesWebMVC.Data;
using SalesWebMVC.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("SalesWebMvcContext");
builder.Services.AddDbContext<SalesWebMVCContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesRecordService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Localization
var enUs = new CultureInfo("en-US");
var locationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUs),
    SupportedCultures = new List<CultureInfo> { enUs },
    SupportedUICultures = new List<CultureInfo> { enUs }
};

app.UseRequestLocalization(locationOptions);

#endregion Localization

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<SeedingService>();
        seeder.Seed();
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using hannahM.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

});

builder.Services.AddSession();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();



app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.UseDeveloperExceptionPage();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}/{slug?}/");
//    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
 
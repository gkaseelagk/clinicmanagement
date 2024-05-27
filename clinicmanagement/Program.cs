using Microsoft.EntityFrameworkCore;

using clinicmanagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
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

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "patients",
    pattern: "patients",
    defaults: new { controller = "Patients", action = "Index" });*/
app.MapControllerRoute(
  name: "default",
pattern: "{controller=Patients}/{action=Index}/{id?}");
/*(app.MapControllerRoute(
    name: "doctors",
    pattern: "doctors",
    defaults: new { controller = "Doctors", action = "Index" })
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Patients}/{action=Index}/{id?}");*/


app.Run();


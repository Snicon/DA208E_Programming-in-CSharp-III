using DA208E_Assignment2.Data;
using DA208E_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// TODO: Remove and replace logic using this object with the db
/*Event eventInfo = new Event() {
    Title = "John & Jane's Wedding",
    Date = DateOnly.FromDateTime(DateTime.Now),
    Time = new TimeOnly().AddHours(15),
    RsvpByDate = new DateOnly(2026, 10, 12),
    Location = "Springfield Wedding Hall, Lund, Sweden"
};*/

// TODO: Remove, see todo above.
// builder.Services.AddSingleton<Event>(eventInfo);

// Registering the db context as a service
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
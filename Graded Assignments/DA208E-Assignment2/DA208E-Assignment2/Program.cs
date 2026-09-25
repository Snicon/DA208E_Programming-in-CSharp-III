using DA208E_Assignment2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Using the same principle as above since this data will be used on multiple pages
Event eventInfo = new Event() {
    Title = "John & Jane's Wedding",
    Date = DateOnly.FromDateTime(DateTime.Now),
    Time = new TimeOnly().AddHours(15),
    RsvpByDate = new DateOnly(2026, 10, 12),
    Location = "Springfield Wedding Hall, Lund, Sweden"
};

builder.Services.AddSingleton<Event>(eventInfo);

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
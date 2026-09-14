// Sixten Peterson (AQ9300) 2026-09-14
using DA208E_Assignment1.Data;
using DA208E_Assignment1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Registers a single instance of GuestService, see first image on p.7 of the Assignment 1 help document.
builder.Services.AddSingleton<GuestService>();

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
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();
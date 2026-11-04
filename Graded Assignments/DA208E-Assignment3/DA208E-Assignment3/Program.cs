// Sixten Peterson (AQ9300) 2026-11-04
using System.Security.Claims;
using DA208E_Assignment3.Data;
using DA208E_Assignment3.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Note to self: p.602 in ASP.NET Core In Action (Third edition) by Andrew Lock
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("GuestManager", policyBuilder => policyBuilder.RequireClaim("CanManageGuests", "true"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Needed for the scaffolded identity pages

// Registering the db context as a service
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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

// Note to self: p.580 in ASP.NET Core In Action (Third edition) by Andrew Lock, p.10 in Assignment 3 - Help document
// from Canvas.
app.UseAuthentication(); // Adds the authentication middleware
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages(); // Needed for the scaffolded identity pages

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seeding below:
// Heavily based on the following online article:
// https://www.c-sharpcorner.com/article/implementing-role-based-authorization-in-asp-net-core-mvc
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await RoleSeeder.SeedRolesASync(roleManager);
    
    // Assigning roles to users:
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var adminUser = await userManager.FindByEmailAsync("admin@admin.admin");
    
    if (adminUser != null){
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        
        // Adding the claim to the user, maybe not the cleanest solution, but it works.
        var claims = await userManager.GetClaimsAsync(adminUser);

        if (!claims.Any(c => c.Type == "CanManageGuests" && c.Value == "true"))
        {
            await userManager.AddClaimAsync(adminUser, new Claim("CanManageGuests", "true"));
        }
    }
}

app.Run();
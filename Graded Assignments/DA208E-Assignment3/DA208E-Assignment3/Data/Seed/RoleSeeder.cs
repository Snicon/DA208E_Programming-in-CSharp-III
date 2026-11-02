using Microsoft.AspNetCore.Identity;

namespace DA208E_Assignment3.Data.Seed;

/// <summary>
/// A simple seeder class for making sure the specified roles are present in the database.
/// 
/// Heavily based on the following online article:
/// https://www.c-sharpcorner.com/article/implementing-role-based-authorization-in-asp-net-core-mvc
/// </summary>
public class RoleSeeder
{
    
    public static async Task SeedRolesASync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
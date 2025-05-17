using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Создание ролей
        string[] roleNames = { "Admin", "Editor", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Создание администратора
        var adminEmail = "admin@example.com";
        var adminPassword = "Admin123!";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Создание эдитора
        var editorEmail = "editor@example.com";
        var editorPassword = "Editor123!";

        if (await userManager.FindByEmailAsync(editorEmail) == null)
        {
            var editorUser = new IdentityUser
            {
                UserName = editorEmail,
                Email = editorEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(editorUser, editorPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(editorUser, "Editor");
            }
        }
    }
}
// SportsStore.Infrastructure/IdentitySeedData.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;

namespace SportsStore.Infrastructure;

public static class IdentitySeedData
{
    private const string adminUser = "Admin";
    private const string adminPassword = "SecretPassword123$";

    public static async void EnsurePopulated(IApplicationBuilder app)
    {
        // Áp dụng các pending migrations cho AppIdentityDbContext nếu có
        AppIdentityDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppIdentityDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        // Lấy UserManager từ DI container để tạo user
        UserManager<AppUser> userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<AppUser>>();

        // Kiểm tra xem user Admin đã tồn tại chưa
        AppUser? user = await userManager.FindByNameAsync(adminUser);

        if (user == null)
        {
            // Tạo mới user Admin với mật khẩu đã định nghĩa
            user = new AppUser { UserName = adminUser };
            await userManager.CreateAsync(user, adminPassword);
        }
    }
}

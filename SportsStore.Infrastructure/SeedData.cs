// SportsStore.Infrastructure/SeedData.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;

namespace SportsStore.Infrastructure;

public static class SeedData
{
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        ApplicationDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Nếu có bất kỳ migration nào đang chờ, hãy áp dụng chúng
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        // Chỉ seed data nếu bảng Products đang trống
        if (!context.Products.Any())
        {
            // AddRange cho phép thêm nhiều đối tượng cùng lúc, hiệu quả hơn Add từng cái.
            context.Products.AddRange(
                new ModelProduct { Name = "Thuyền Kayak", Description = "Chiếc thuyền nhỏ cho một người chèo.", Category = "Thể thao dưới nước", Price = 6600000m, ImageUrl = "/images/kayak.png" },
                new ModelProduct { Name = "Áo phao cứu sinh", Description = "Bảo hộ an toàn, kiểu dáng thời trang.", Category = "Thể thao dưới nước", Price = 1175000m, ImageUrl = "/images/lifejacket.png" },
                new ModelProduct { Name = "Bóng đá tiêu chuẩn", Description = "Bóng đạt chuẩn kích thước và trọng lượng FIFA.", Category = "Bóng đá", Price = 470000m, ImageUrl = "/images/soccer-ball.png" },
                new ModelProduct { Name = "Cờ góc sân", Description = "Tạo vẻ chuyên nghiệp cho sân bóng của bạn.", Category = "Bóng đá", Price = 840000m, ImageUrl = "/images/corner-flag.png" },
                new ModelProduct { Name = "Mô hình sân vận động", Description = "Sân vận động 35,000 chỗ ngồi (đóng gói phẳng).", Category = "Bóng đá", Price = 1908000000m, ImageUrl = "/images/stadium.png" },
                new ModelProduct { Name = "Mũ Tư Duy", Description = "Cải thiện 75% hiệu suất hoạt động của não.", Category = "Cờ Vua", Price = 385000m, ImageUrl = "/images/chess-board.png" },
                new ModelProduct { Name = "Ghế không vững", Description = "Bí mật gây bất lợi cho đối thủ của bạn.", Category = "Cờ Vua", Price = 720000m, ImageUrl = "/images/unstable-chair.png" },
                new ModelProduct { Name = "Bàn cờ người", Description = "Trò chơi thú vị cho cả gia đình.", Category = "Cờ Vua", Price = 1800000m, ImageUrl = "/images/chess-board.png" },
                new ModelProduct { Name = "Quân Vua Kim Cương", Description = "Quân Vua mạ vàng, đính kim cương.", Category = "Cờ Vua", Price = 28800000m, ImageUrl = "/images/chess-king.png" },
                new ModelProduct { Name = "Giày chạy bộ", Description = "Nhẹ và thoải mái cho quãng đường dài.", Category = "Chạy bộ", Price = 2400000m, ImageUrl = "/images/running-shoes.png" },
                new ModelProduct { Name = "Thảm Yoga cao cấp", Description = "Bề mặt chống trượt giúp giữ thăng bằng hoàn hảo.", Category = "Fitness", Price = 840000m, ImageUrl = "/images/yoga-mat.png" },
                new ModelProduct { Name = "Bình nước giữ nhiệt", Description = "Giữ lạnh đồ uống trong 24 giờ.", Category = "Fitness", Price = 380000m, ImageUrl = "/images/water-bottle.png" }
            );

            // Quan trọng: Mọi thay đổi (Add, Update, Remove) chỉ nằm trong bộ nhớ
            // cho đến khi bạn gọi SaveChanges(). Lệnh này sẽ tạo và gửi
            // các câu lệnh INSERT tương ứng đến CSDL.
            context.SaveChanges();
        }
    }
}

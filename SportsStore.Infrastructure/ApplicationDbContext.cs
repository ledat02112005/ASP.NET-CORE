// SportsStore.Infrastructure/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain;

namespace SportsStore.Infrastructure;

public class ApplicationDbContext : DbContext
{
    // Constructor này nhận các tùy chọn cấu hình (như chuỗi kết nối) từ bên ngoài
    // thông qua Dependency Injection.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Khai báo với EF Core rằng chúng ta có một bảng tương ứng với model ModelProduct.
    // EF Core sẽ tự động đặt tên bảng là "Products" (dạng số nhiều).
    public DbSet<ModelProduct> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Chỉ định độ chính xác cho cột Price để tránh bị cắt bớt dữ liệu
        modelBuilder.Entity<ModelProduct>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}

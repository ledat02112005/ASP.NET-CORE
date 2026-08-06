// SportsStore.Infrastructure/EFProductRepository.cs
using SportsStore.Domain;

namespace SportsStore.Infrastructure;

public class EFProductRepository : IProductRepository // Implement cùng interface với Fake repo
{
    // Inject DbContext để làm việc với CSDL
    private ApplicationDbContext _context;

    public EFProductRepository(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    // Triển khai thuộc tính Products được yêu cầu bởi interface.
    // Thay vì trả về một List hard-code, giờ đây nó trả về một DbSet.
    // EF Core sẽ dịch các truy vấn LINQ trên DbSet này thành câu lệnh SQL.
    public IQueryable<ModelProduct> Products => _context.Products;
}

using SportsStore.Domain;

namespace SportsStore.Infrastructure
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        // DI Container sẽ tự động inject ApplicationDbContext vào đây
        public EFProductRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        // Trả về dữ liệu Products thực từ CSDL thay vì dữ liệu giả
        public IQueryable<ModelProduct> Products => _context.Products;
    }
}

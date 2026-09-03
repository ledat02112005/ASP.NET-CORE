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

    public void SaveProduct(ModelProduct product)
    {
        if (product.ProductID == 0)
        {
            _context.Products.Add(product);
        }
        else
        {
            ModelProduct? dbEntry = _context.Products
                .FirstOrDefault(p => p.ProductID == product.ProductID);

            if (dbEntry != null)
            {
                dbEntry.Name = product.Name;
                dbEntry.Description = product.Description;
                dbEntry.Price = product.Price;
                dbEntry.Category = product.Category;
                dbEntry.ImageUrl = product.ImageUrl;
            }
        }
        _context.SaveChanges();
    }

    public ModelProduct? DeleteProduct(int productID)
    {
        ModelProduct? dbEntry = _context.Products
            .FirstOrDefault(p => p.ProductID == productID);

        if (dbEntry != null)
        {
            _context.Products.Remove(dbEntry);
            _context.SaveChanges();
        }

        return dbEntry;
    }
}

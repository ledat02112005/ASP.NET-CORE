using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain;

namespace SportsStore.Infrastructure
{
    public class FakeProductRepository : IProductRepository
    {
        // Triển khai thuộc tính Products mà hợp đồng yêu cầu.
        public IQueryable<ModelProduct> Products => new List<ModelProduct>
        {
            new ModelProduct {
                ProductID = 1,
                Name = "Football", Description = "FIFA-approved size and weight",
                Price = 25, Category = "Soccer", ImageUrl ="/images/football.png"
            },

            new ModelProduct {
                ProductID = 2,
                Name = "Surf Board", Description = "A board for riding the waves",
                Price = 179, Category = "Surfing", ImageUrl ="/images/surfboard.png"
            },

            new ModelProduct {
                ProductID = 3,
                Name = "Running Shoes", Description = "Comfortable and stylish running shoes",
                Price = 95, Category = "Running", ImageUrl ="/images/runningshoes.png"
            },

            new ModelProduct {
                ProductID = 4,
                Name = "Kayak", Description = "A boat for one person",
                Price = 275, Category = "Watersports", ImageUrl ="/images/kayak.png"
            },

            new ModelProduct {
                ProductID = 5,
                Name = "Corner Flags", Description = "Give your playing field a professional touch",
                Price = 34.95m, Category = "Soccer", ImageUrl ="/images/cornerflags.png"
            },
        }.AsQueryable(); // Chuyển List thành IQueryable

        // Fake implementations — không thao tác dữ liệu thật
        public void SaveProduct(ModelProduct product) { }

        public ModelProduct? DeleteProduct(int productID) => null;
    }
}

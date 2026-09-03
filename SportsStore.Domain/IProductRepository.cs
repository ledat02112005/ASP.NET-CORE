using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain
{
    public interface IProductRepository
    {
        IQueryable<ModelProduct> Products { get; }

        void SaveProduct(ModelProduct product);

        ModelProduct? DeleteProduct(int productID);
    }
}

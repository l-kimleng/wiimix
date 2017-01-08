using System.Collections.Generic;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Display();
        Product FindUpdate(int productId);

    }
}
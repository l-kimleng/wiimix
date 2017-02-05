using System.Collections.Generic;
using WiiMix.Business.Model;

namespace WiiMix.SaleInventory.Service
{
    public interface IProductService
    { 
        IEnumerable<Product> Find();
        Product FindUpdate(int id);
        int Update(Product product);
        Product Add(Product product);
    }
}
using System.Collections.Generic;
using WiiMix.Business.Model;

namespace WiiMix.SaleInventory.Service
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAll();
    }
}
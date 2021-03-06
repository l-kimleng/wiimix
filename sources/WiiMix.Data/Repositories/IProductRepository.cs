﻿using System.Collections.Generic;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Find();
        Product FindUpdate(int productId);

    }
}
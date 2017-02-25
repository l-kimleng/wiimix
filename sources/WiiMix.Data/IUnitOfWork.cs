using System;
using WiiMix.Data.Repositories;

namespace WiiMix.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IBrandRepository Brands { get; }
        IConfigRepository Configs { get; }
        IStockRepository Stocks { get; }
        int Completed();
    }
}
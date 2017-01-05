using System;
using WiiMix.Data.Repositories;

namespace WiiMix.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }
        int Completed();
    }
}
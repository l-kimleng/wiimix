using System;
using WiiMix.Data.Repositories;

namespace WiiMix.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        int Completed();
    }
}
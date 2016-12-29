using System;

namespace WiiMix.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int Completed();
    }
}
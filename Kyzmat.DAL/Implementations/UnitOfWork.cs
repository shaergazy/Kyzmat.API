using Kyzmat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace Kyzmat.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly ConcurrentDictionary<Type, object> _repsDictionary;
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repsDictionary = new ConcurrentDictionary<Type, object>();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (_repsDictionary.TryGetValue(typeof(TEntity), out object repository))
                return (IGenericRepository<TEntity>)repository;
            repository = new GenericRepository<TEntity>(_dbContext);
            _repsDictionary.TryAdd(typeof(TEntity), repository);
            return (IGenericRepository<TEntity>)repository;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        ~UnitOfWork() => Dispose(false);
    }
}

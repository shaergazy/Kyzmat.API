using Microsoft.EntityFrameworkCore.Storage;

namespace Kyzmat.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        public Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

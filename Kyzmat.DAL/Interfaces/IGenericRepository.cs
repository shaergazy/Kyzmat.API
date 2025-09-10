using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Kyzmat.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression = null);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        Task<TEntity> FirstOrDefaultAsync();

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        void AddRange(ICollection<TEntity> entities);

        void DeleteRange(ICollection<TEntity> entities);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(ICollection<TEntity> entities);

        bool Any(Expression<Func<TEntity, bool>> expression = null);

        IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);

        public IQueryable<TEntity> AsNoTracking();
    }
}

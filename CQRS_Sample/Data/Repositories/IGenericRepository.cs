using CQRS_Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRS_Sample.Data.Repositories
{
    public interface IGenericRepository<TEntity, TContext> where TEntity : IEntity where TContext : DbContext
    {
        TEntity? Get(Expression<Func<TEntity,bool>> query);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> query, CancellationToken token);        
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> query);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> query,CancellationToken token);        
        void Add(TEntity entity);     
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);        
        void Delete(TEntity entity);
        void Save();
        Task SaveAsync(CancellationToken token);        

    }
}

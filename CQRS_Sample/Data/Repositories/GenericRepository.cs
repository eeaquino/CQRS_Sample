using CQRS_Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRS_Sample.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext> where TEntity:class, IEntity where TContext: DbContext
    {
        private readonly TContext _context;
        private DbSet<TEntity> _table;
        public GenericRepository(TContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _table.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            if (!entities.Any())
            {
                throw new ArgumentException("entities");
            }
            _table.AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _table.Remove(entity);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> query)
        {
            return _table.SingleOrDefault(query);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> query, CancellationToken token)
        {
            return await _table.SingleOrDefaultAsync(query,token);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> query)
        {
            return _table.Where(query).AsEnumerable();
        }

        public async  Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> query, CancellationToken token)
        {
            return await _table.Where(query).ToListAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _table.Attach(entity);
        }
    }
}

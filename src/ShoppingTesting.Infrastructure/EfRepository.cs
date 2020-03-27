using System;
using System.Linq;
using System.Threading.Tasks;
using ShoppingTesting.Domain;
using ShoppingTesting.Domain.Exception;
using ShoppingTesting.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace ShoppingTesting.Infrastructure
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        private readonly ShoppingTestingContext _context;

        public EfRepository(ShoppingTestingContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll(params string[] includes)
        {
            var dbSet = _context.Set<TEntity>().AsQueryable();
            dbSet = ApplyIncludes(dbSet, includes);
            return dbSet;
        }

        public async Task<TEntity> GetById(TKey id, params string[] includes)
        {
            var dbSet = _context.Set<TEntity>().AsQueryable();
            dbSet = ApplyIncludes(dbSet, includes);
            try
            {
                return await dbSet.SingleAsync(entity => entity.Id.Equals(id));
            }
            catch (InvalidOperationException exception)
            {
                throw new DomainException($"Entity with {id.ToString()} not found!", exception);
            }
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> dbSet, string[] includes) =>
            includes.Where(include => include != string.Empty)
                .Aggregate(dbSet, (current, include) => current.Include(include));
    }
}
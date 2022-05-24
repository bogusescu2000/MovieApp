using Behaviour.Interfaces;
using DataAccess.Data;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Behaviour.Abstract
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdWithIncludes(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            if (id == 0) return null;

            IQueryable<T> entities = _context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var include in includeProperties)
                {
                    entities = entities.Include(include);
                }
            }
            return await entities.FirstOrDefaultAsync(i => i.Id == id);
        }

        public List<T> GetAllWithIncludes(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = _context.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            }

            list = dbQuery.AsNoTracking().ToList<T>();

            return list;
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Entities.Entities;
using System.Linq.Expressions;

namespace Behaviour.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Add(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task Update(T entity);
        Task Delete(int id);
        Task SaveChangesAsync();
        Task<T> GetByIdWithIncludes(int id, params Expression<Func<T, object>>[] includeProperties);
        List<T> GetAllWithIncludes(params Expression<Func<T, object>>[] navigationProperties);
    }
}

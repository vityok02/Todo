using Todo.Mvc.Models;

using Task = System.Threading.Tasks.Task;

namespace Todo.Mvc.Data;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

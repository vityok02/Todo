namespace Todo.Mvc.Data;

public interface ITaskRepository : IRepository<Models.Task>
{
    Task<IEnumerable<Models.Task?>> GetAllAsync(string? param, CancellationToken cancellationToken = default);
}

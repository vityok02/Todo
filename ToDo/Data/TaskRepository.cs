using Microsoft.EntityFrameworkCore;
using Todo.Data;

namespace Todo.Mvc.Data;

public class TaskRepository : Repository<Models.Task>, ITaskRepository
{
    private readonly AppDbContext _dbContext;

    public TaskRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Task?>> GetAllAsync(string? param, CancellationToken cancellationToken = default)
    {
        if (param is null)
        {
            return await GetAllAsync(cancellationToken);
        }

        return await
            _dbContext.Tasks
            .Where(t => t.Title.ToLower()
                .Contains(param.ToLower()))
            .ToArrayAsync(cancellationToken);
    }
}

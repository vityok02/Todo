using Microsoft.EntityFrameworkCore;

using Task = Todo.Mvc.Models.Task;

namespace Todo.Data;

public class AppDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var taskBuilder = builder.Entity<Task>();

        taskBuilder.HasKey(x => x.Id);

        taskBuilder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        taskBuilder.Ignore(t => t.Status);
    }
}

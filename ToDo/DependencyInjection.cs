﻿using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Mvc.Data;

using Task = Todo.Mvc.Models.Task;

namespace Todo;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("docker");

        return services
            .AddDbContext<AppDbContext>(o =>
                o.UseSqlServer(connectionString))
            .AddScoped<IRepository<Task>, Repository<Task>>()
            .AddScoped<ITaskRepository, TaskRepository>()
            ;
    }
}

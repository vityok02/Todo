using Microsoft.AspNetCore.Mvc;
using Todo.Mvc.Data;
using Todo.Mvc.Models.Dtos;
using Task = Todo.Mvc.Models.Task;

namespace Todo.Mvc.Controllers;

public class TasksController : Controller
{
    private const string TaskNotFoundMessage = "Task not found";
    private readonly ITaskRepository _taskRepository;

    public TasksController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    [Route("")]
    [Route("tasks/")]
    public async Task<IActionResult> List(
        [FromQuery] string? title,
        CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetAllAsync(title, cancellationToken);

        if (!tasks.Any())
        {
            return View(Array.Empty<Task>());
        }

        return View(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] string? title,
        CancellationToken cancellationToken = default)
    {
        if (title is null)
        {
            return RedirectToAction(nameof(List));
        }

        if (!ModelState.IsValid)
        {
            return View(nameof(List));
        }

        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        if (tasks.Any(t => t!.Title == title))
        {
            return BadRequest("Task with such title already exists");
        }

        var task = new Task(title);

        await _taskRepository.CreateAsync(task, cancellationToken);

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    [Route("tasks/edit/{id:int}")]
    public async Task<IActionResult> Edit(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return NotFound(TaskNotFoundMessage);
        }

        var task = await _taskRepository.GetAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound(TaskNotFoundMessage);
        }

        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        [FromRoute] int id,
        [FromForm] EditTaskDto taskDto,
        CancellationToken cancellationToken = default)
    {
        if (id == 0)
        {
            return NotFound(TaskNotFoundMessage);
        }

        var task = await _taskRepository.GetAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound(TaskNotFoundMessage);
        }

        if (!ModelState.IsValid)
        {
            return View(taskDto);
        }

        task.UpdateTask(taskDto.Title);
        await _taskRepository.UpdateAsync(task, cancellationToken);

        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        if (id == 0)
        {
            return NotFound(TaskNotFoundMessage);
        }

        var task = await _taskRepository.GetAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound(TaskNotFoundMessage);
        }

        await _taskRepository.DeleteAsync(task, cancellationToken);

        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public async Task<IActionResult> Complete(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        if (id == 0)
        {
            return NotFound(TaskNotFoundMessage);
        }

        var task = await _taskRepository.GetAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound(TaskNotFoundMessage);
        }

        task.CompleteTask();
        await _taskRepository.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(List));
    }
}

using System.ComponentModel.DataAnnotations;

namespace Todo.Mvc.Models;

public class Task : BaseEntity
{
    public string Title { get; private set; } = "";
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; private set; }
    public bool IsCompleted { get; private set; } = false;
    public string Status => IsCompleted ? "Completed" : "Not completed";

    public Task()
    {
    }

    public Task(string title)
    {
        Title = title;
        CreatedDate = DateTime.Now;
    }

    public void CompleteTask()
    {
        IsCompleted = true;
    }

    public void UpdateTask(string? title)
    {
        Title = title ?? Title;
    }
}

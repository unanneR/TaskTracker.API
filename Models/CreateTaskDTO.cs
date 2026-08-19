namespace TaskTracker.API.Models;

public class CreateTaskDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}
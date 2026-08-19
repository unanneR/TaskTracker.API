using Microsoft.EntityFrameworkCore;

namespace TaskTracker.API.Models;

public class ProjectContext(DbContextOptions<ProjectContext> options) : DbContext(options)
{
    public DbSet<ProjectTask> ProjectTasks { get; set;} = null!;
}
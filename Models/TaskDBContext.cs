using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class TaskDBContext : DbContext
    {
        public TaskDBContext(DbContextOptions <TaskDBContext> options) :base (options){
        }

        public DbSet<TaskModel> Tasks { get; set; } = null!;
        public DbSet<UserModel> Users { get; set; } = null!;
    }
}
 
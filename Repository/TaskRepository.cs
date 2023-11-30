using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebAPI.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDBContext _dbContext;
        public TaskRepository(TaskDBContext context) {
            _dbContext = context;
        }

        public async Task AddTaskAsync(TaskDTO taskDTO)
        {
            var newTask = new TaskModel
            {
                Id = Guid.NewGuid(),
                Nama = taskDTO.Nama,
                Tugas = taskDTO.Tugas,
                Deskripsi = taskDTO.Deskripsi,
                TanggalDeadline = taskDTO.TanggalDeadline
            };
            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task DeleteTaskAsync(Guid uId)
        {
            var task = await _dbContext.Tasks.FindAsync(uId);
            if(task == null)
            {
                return;
            }
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TaskDTO> GetTaskByIdAsync(Guid uId)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(u => u.Id == uId);

            if (task == null)
            {
                return null ;
            }

            return new TaskDTO
            {
                Id = task.Id,
                Nama = task.Nama,
                Tugas = task.Tugas,
                Deskripsi = task.Deskripsi,
                TanggalDeadline = task.TanggalDeadline
            };
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
        {
            var _tasks = await _dbContext.Tasks.ToListAsync();
            return _tasks.Select(task => new TaskDTO
            {
                Id = task.Id,
                Nama = task.Nama,
                Tugas = task.Tugas,
                Deskripsi = task.Deskripsi,
                TanggalDeadline = task.TanggalDeadline
            }) ;
        }

        public async Task UpdateTaskAsync(Guid uId, TaskDTO taskDTO)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(uId);
            if(existingTask == null)
            {
                return;
            }
            existingTask.Nama = taskDTO.Nama;
            existingTask.Tugas = taskDTO.Tugas;
            existingTask.Deskripsi = taskDTO.Deskripsi;
            existingTask.TanggalDeadline = taskDTO.TanggalDeadline;
            _dbContext.Tasks.Update(existingTask);
            await _dbContext.SaveChangesAsync();
        }
    }
}

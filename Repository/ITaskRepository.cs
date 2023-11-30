using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDTO>> GetTasksAsync(); // Change the return type
        Task<TaskDTO> GetTaskByIdAsync(Guid uId);
        Task AddTaskAsync(TaskDTO taskDTO);
        Task UpdateTaskAsync(Guid uId,TaskDTO taskDTO); 
        Task DeleteTaskAsync(Guid uId);
    }

}


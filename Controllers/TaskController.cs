using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using WebAPI.Models;
using WebAPI.Repository;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _tr;
        public TaskController(ITaskRepository tr)
        {
            _tr = tr;
        }
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var taskDTOs = await _tr.GetTasksAsync();
            return Ok(taskDTOs);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTaskById(Guid Id)
        {
            var userDtos = await _tr.GetTaskByIdAsync(Id);
            return Ok(userDtos);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO taskDto)
        {

            await _tr.AddTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskDto.Id }, taskDto);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTask(Guid Id, [FromBody] TaskDTO taskDto)
        {
            await _tr.UpdateTaskAsync(Id, taskDto);
            return Ok("Successfully Update");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            await _tr.DeleteTaskAsync(Id);
            return Ok("Successfully Deleted");
        }
    }
}

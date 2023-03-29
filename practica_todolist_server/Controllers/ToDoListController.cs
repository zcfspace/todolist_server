using Microsoft.AspNetCore.Mvc;
using practica_todolist_server.Models;

namespace practica_todolist_server.Controllers
{
    [Route("api/[Action]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskModel>> GetTasks()
        {
            try
            {
                List<TaskModel> tasks = new List<TaskModel>();
                tasks.Add(new TaskModel() { id = 1, task = "Tarea 1", completed = true});
                tasks.Add(new TaskModel() { id = 2, task = "Tarea 2", completed = false });
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<TaskModel> InsertTask(TaskModel task)
        {
            try
            {
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TaskModel> UpdateTask(int id, TaskModel task)
        {
            try
            {
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/complete")]
        public ActionResult CompleteTask(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

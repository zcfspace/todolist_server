using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using practica_todolist_server.Data;
using practica_todolist_server.Models;

namespace practica_todolist_server.Controllers
{
    [Route("api/[Action]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ToDoListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<TaskModel>> GetTasks()
        {
            try
            {
                List<TaskModel> tasks = new List<TaskModel>();

                using (SqlConnection connection = Database.GetConnection(_configuration))
                {
                    string query = "SELECT id, task, complete FROM Task";

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TaskModel task = new TaskModel();
                        task.id = reader.GetInt32(0);
                        task.task = reader.GetString(1);
                        task.complete = reader.GetBoolean(2);
                        tasks.Add(task);
                    }
                    connection.Close();
                }
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
                using (SqlConnection connection = Database.GetConnection(_configuration))
                {
                    string query = "INSERT INTO Task (task, complete) VALUES (@Task, @Complete); SELECT CAST(scope_identity() AS int)";

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Task", task.task);
                    command.Parameters.AddWithValue("@Complete", task.complete);

                    int taskId = (int)command.ExecuteScalar();
                    task.id = taskId;

                    connection.Close();

                    return Ok(task);
                }

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
                using (SqlConnection connection = Database.GetConnection(_configuration))
                {
                    string query = "UPDATE Task SET task = @Task WHERE id = @Id";

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Task", task.task);
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return Ok(task);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public ActionResult CompleteTask(int id)
        {
            try
            {
                using (SqlConnection connection = Database.GetConnection(_configuration))
                {
                    string query = "UPDATE Task SET complete = CASE WHEN complete = 1 THEN 0 ELSE 1 END WHERE id = @Id";

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                using (SqlConnection connection = Database.GetConnection(_configuration))
                {
                    string query = "DELETE FROM Task WHERE id = @Id";

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

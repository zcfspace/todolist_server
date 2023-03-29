// using System;
// using System.Net.Http;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("api/[controller]")]
// public class EjemploController : ControllerBase
// {
//     private readonly HttpClient _httpClient;

//     public EjemploController()
//     {
//         _httpClient = new HttpClient();
//     }

//     [HttpPost]
//     public async Task<IActionResult> CrearTarea([FromBody] Tarea tarea)
//     {
//         try
//         {
//             // Crear los datos que deseas enviar en el cuerpo de la petición (payload)
//             var data = new StringContent($"{{\"name\": \"{tarea.Nombre}\", \"description\": \"{tarea.Descripcion}\"}}", System.Text.Encoding.UTF8, "application/json");

//             // Realizar la petición HTTP POST
//             var response = await _httpClient.PostAsync("http://localhost:8000/api/tareas", data);

//             // Leer la respuesta del servidor
//             var result = await response.Content.ReadAsStringAsync();

//             return Ok(result); // Devolver la respuesta en el cuerpo de la respuesta HTTP
//         }
//         catch (Exception ex)
//         {
//             return BadRequest($"Error al enviar la petición: {ex.Message}");
//         }
//     }
// }

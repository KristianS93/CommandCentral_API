// using Domain.Entities;
// using Infrastructure.Interfaces;
// using Infrastructure.Services;
// using Microsoft.AspNetCore.Mvc;
//
// namespace API.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class TodoController : ControllerBase
// {
//     private readonly ITodoService _repository;
//     private readonly ILogger<TodoController> _logger;
//     public TodoController(ITodoService repository, ILogger<TodoController> logger)
//     {
//         _repository = repository;
//         _logger = logger;
//     }
//
//     [HttpGet(Name = "GetTodoItems")]
//     public async Task<List<TodoItem>> GetTodoItems()
//     {
//         _logger.LogWarning("In gettodo");
//         var todoItems = await _repository.GetAllAsync();
//         return todoItems;
//     }
//     
//     [HttpGet("{id}")]
//     public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
//     {
//         var todoItem = await _repository.GetByIdAsync(id);
//         if (todoItem == null)
//         {
//             return NotFound();
//         }
//         return Ok(todoItem);
//     }
//     
//     [HttpPost]
//     public async Task<IActionResult> CreateTodoItem(TodoItem todoItem)
//     {
//         await _repository.CreateAsync(todoItem);
//         return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
//     }
//     
//     [HttpPut("{id}")]
//     public async Task<IActionResult> UpdateTodoItem(int id, TodoItem todoItem)
//     {
//         if (id != todoItem.Id)
//         {
//             return BadRequest();
//         }
//     
//         await _repository.UpdateAsync(todoItem);
//         return NoContent();
//     }
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteTodoItem(int id)
//     {
//         var todoItem = await _repository.GetByIdAsync(id);
//         if (todoItem == null)
//         {
//             return NotFound();
//         }
//     
//         await _repository.DeleteAsync(todoItem);
//         return NoContent();
//     }
//     
// }
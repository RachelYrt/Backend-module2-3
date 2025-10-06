using Microsoft.AspNetCore.Mvc;
using ToDoApi.DTOs;
using ToDoApi.Data;

namespace ToDoApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class TodoController:ControllerBase
    {
        //todo refactor this controller into another service layer
        //move all the query to the service, service should return DTO to controller
        //service should use depedency injection into controller

        //not use todoservice for CRUD
        private readonly TodoService _todoService;
        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
        {
            var todos = await _todoService.GetAllAsync();
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById([FromRoute]string id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            
            if (todo is null)
                return NotFound();
            return Ok(todo);

        }
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateNew([FromBody] CreateTodoDto dto)
        {
            try
            {
                var todo = await _todoService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]UpdateTodoDto dto)
        {
            var updated = await _todoService.UpdateAsync(id,dto);
            if (updated == null)
            {
                return NotFound();
            }
            
            return Ok(updated);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] string id)
        {
            var success = await _todoService.DeleteAsync(id);
            if(!success )
            {
                return NotFound();
            }
            return NoContent();

        }

    }
}

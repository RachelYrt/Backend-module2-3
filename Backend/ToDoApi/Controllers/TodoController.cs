using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Commands;
using ToDoApi.Data;
using ToDoApi.DTOs;
using ToDoApi.Mappers;
using ToDoApi.Models;

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
        private readonly TodoContext _context;
        private readonly ILoggerFactory _loggerFactory;
        public TodoController(TodoContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
        {
            var todos = await _context.Todos
                .Include(t => t.Category)
                .Select(t => t.ToDto())
                .ToListAsync();
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById([FromRoute]string id)
        {
            var todo = await _context.Todos
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (todo is null)
                return NotFound();
            return Ok(todo.ToDto());

        }
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateNew([FromBody] CreateTodoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
                return NotFound();
            var todo = new Todo
            {
                Text = dto.Text,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
            };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            var created = await _context.Todos
                .Include(t => t.Category)
                .FirstAsync(T => T.Id == todo.Id);
            return CreatedAtAction(nameof(GetById),new {id = created.Id}, created.ToDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> Update(string id, [FromBody]UpdateTodoDto dto)
        {
            var updated = await _context.Todos.FindAsync(id);
            if(updated == null)
            {
                return NotFound();
            }
            updated.Text = dto.Text;
            updated.Description = dto.Description;
            updated.CategoryId = dto.CategoryId;
            _context.Todos.Update(updated);
            await _context.SaveChangesAsync();
            
            return Ok(updated);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] string id)
        {
            var success = await _context.Todos.FindAsync(id);
            if(success == null )
            {
                return NotFound();
            }
            _context.Todos.Remove(success);
            await _context.SaveChangesAsync();
            
            return NoContent();

        }

    }
}

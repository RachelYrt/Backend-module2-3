using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class TodoController:ControllerBase
    {
        private readonly TodoContext _context; 
        public TodoController(TodoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var todo = await _context.Todos
                .Include(x => x.Category)
                .ToListAsync();
            return Ok(todo);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute]string id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);

        }
        [HttpPost]
        public async Task<ActionResult> CreateNew([FromBody] Todo todo)
        {
            if (todo == null)
            {
                return NotFound();
            }
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id = todo.Id}, todo);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(string id, [FromBody]Todo updateTodo)
        {
            var todo = await _context.Todos.FindAsync(id);
            if(todo == null)
            {
                return NotFound();
            }
            todo.Text = updateTodo.Text;
            todo.Description = updateTodo.Description;
            await _context.SaveChangesAsync();
            return Ok(todo);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo([FromRoute] string id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if(todo == null)
            {
                return NotFound();
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}

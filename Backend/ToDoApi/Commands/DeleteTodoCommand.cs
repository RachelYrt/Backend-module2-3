using ToDoApi.Models;
using ToDoApi.Data;
using ToDoApi.Mappers;
using MediatR;
using ToDoApi.DTOs;

namespace ToDoApi.Commands
{

    public class DeleteTodoCommand
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;
        public DeleteTodoCommand(TodoContext context,ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> ExecuteAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                _logger.LogInformation("Todo not found: {Id}", id);
                return false;
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            _logger.LogInformation("ToDo deleted successfully: {Id}", id);
            return true;
        }



    }
}

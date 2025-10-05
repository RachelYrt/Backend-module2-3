using ToDoApi.Models;
using ToDoApi.Mappers;
using ToDoApi.Data;
using ToDoApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Queries
{
    public class GetTodoByIdQuery
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;
        public GetTodoByIdQuery(TodoContext context,ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TodoDto?> ExecuteAsync(string id)
        {
            _logger.LogInformation("Fetching todo by Id: {Id}", id);
            var todo = await _context.Todos
                .Include(t=>t.Category)
                .FirstOrDefaultAsync(t=>t.Id == id);
            
            return todo?.ToDto();

        }
    }
}

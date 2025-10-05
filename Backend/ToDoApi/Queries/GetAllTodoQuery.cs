using ToDoApi.DTOs;
using ToDoApi.Data;
using ToDoApi.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Queries
{


    public class GetAllTodoQuery
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;
        public GetAllTodoQuery(TodoContext context,ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<TodoDto>> ExecuteAsync()
        {
            var todos = await _context.Todos
                .Include(t => t.Category)
                .ToListAsync();
            return todos.Select(t=>t.ToDto());
        }

    }
}

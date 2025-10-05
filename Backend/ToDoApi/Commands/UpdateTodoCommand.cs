using System.ComponentModel.DataAnnotations;
using ToDoApi.DTOs;
using ToDoApi.Models;
using ToDoApi.Mappers;

using ToDoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Commands
{

    public class UpdateTodoCommand
    {
        private readonly TodoContext _context;
        private readonly ILogger  _logger;
        public UpdateTodoCommand(TodoContext context,ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TodoDto?> ExecuteAsync(string id, string text, string? description, int? categoryId)
        {
            var todo = await _context.Todos
                .FindAsync(id);
            if(todo == null)
            {
                _logger.LogInformation("Todo not found:{id}",id);
                return null;
            }
            todo.Text = text;
            todo.Description = description;
            todo.CategoryId = categoryId;

            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            var result = await _context.Todos
                .Include(t => t.Category)
                .FirstAsync(t => t.Id == id);
            _logger.LogInformation("Todo updated successfully:{Id}", id);
            return todo.ToDto();
        }
    }
}

using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Mappers;
using ToDoApi.DTOs;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Commands
{

    public class CreateTodoCommand
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;
        public CreateTodoCommand(TodoContext context, ILogger<CreateTodoCommand> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TodoDto> ExecuteAsync(CreateTodoDto dto)
        {
            _logger.LogInformation("Creating new Todo:{Text}",dto.Text);
            var todo = new Todo()
            {
                Text = dto.Text,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Todo created successfully with Id:{Id}", todo.Id);
            return todo.ToDto();


        }
    }
}

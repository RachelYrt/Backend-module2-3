using MediatR;
using System.ComponentModel.DataAnnotations;
using ToDoApi.DTOs;
using ToDoApi.Models;
using ToDoApi.Mappers;

using ToDoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Commands
{
    public class UpdateTodoCommand:IRequest<TodoDto>
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        public string?  Drescription { get; set; }
        public int? CategoryId { get; set; }
    }
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, TodoDto?>
    {
        private readonly TodoContext _context;
        private readonly ILogger<UpdateTodoCommandHandler> _logger;
        public UpdateTodoCommandHandler(TodoContext context,ILogger<UpdateTodoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TodoDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Todo: {Id}",request.Id);
            var todo = await _context.Todos
                .Include(t=>t.Category)
                .FirstOrDefaultAsync(t=>t.Id == request.Id,cancellationToken);
            if(todo == null)
            {
                _logger.LogInformation("Todo not found:{Id}",request.Id);
                return null;
            }
            todo.Text = request.Text;
            todo.Description = request.Drescription;
            todo.CategoryId = request.CategoryId;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Todo updated successfully:{Id}", todo.Id);
            return todo.ToDto();
        }
    }
}

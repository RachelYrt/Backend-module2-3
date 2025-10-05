using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Mappers;
using ToDoApi.DTOs;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Commands
{
    public class CreateTodoCommand:IRequest<TodoDto>
    {
        public string Text {  get; set; }=string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }


    }
    public class CreateTodoCommandHandler:IRequestHandler<CreateTodoCommand,TodoDto>
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;
        public CreateTodoCommandHandler(TodoContext context, ILogger<CreateTodoCommand> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TodoDto> Handle(CreateTodoCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new Todo:{Text}",request.Text);
            var todo = new Todo()
            {
                Text = request.Text,
                Description = request.Description,
                CategoryId = request.CategoryId
            };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync(cancellationToken);
            var result = await _context.Todos
                .Include(t => t.Category)
                .FirstAsync(t => t.Id == todo.Id, cancellationToken);
            _logger.LogInformation("Todo created successfully with Id:{Id}", todo.Id);
            return result.ToDto();


        }
    }
}

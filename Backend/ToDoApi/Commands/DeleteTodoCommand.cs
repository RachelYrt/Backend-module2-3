using ToDoApi.Models;
using ToDoApi.Data;
using ToDoApi.Mappers;
using MediatR;
using ToDoApi.DTOs;

namespace ToDoApi.Commands
{
    public class DeleteTodoCommand:IRequest<bool>
    {
        public string Id { get; set; }= string.Empty;
        public DeleteTodoCommand(string id)
        {
            Id = id;
        }
    }
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly TodoContext _context;
        private readonly ILogger<DeleteTodoCommandHandler> _logger;
        public DeleteTodoCommandHandler(TodoContext context,ILogger<DeleteTodoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting todo: {Id}", request.Id);
            var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);
            if (todo == null)
            {
                _logger.LogInformation("Todo not found: {Id}", request.Id);
                return false;
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("ToDo deleted successfully: {Id}", request.Id);
            return true;
        }



    }
}

using ToDoApi.Models;
using ToDoApi.Mappers;
using ToDoApi.Data;
using MediatR;
using ToDoApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Queries
{
    public record GetByIdQuery(string Id):IRequest<TodoDto?>;
    public class GetByIdQueryHandler : IRequestHandler< GetByIdQuery,TodoDto?>
    {
        private readonly TodoContext _context;
        private readonly ILogger<GetByIdQueryHandler> _logger;
        public GetByIdQueryHandler(TodoContext context,ILogger<GetByIdQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TodoDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching todo by Id: {Id}", request.Id);
            var todo = await _context.Todos
                .Include(t=>t.Category)
                .FirstOrDefaultAsync(t=>t.Id == request.Id,cancellationToken);
            if(todo == null)
            {
                _logger.LogInformation("Todo {Id} not found", request.Id);
                return null;
            }
            _logger.LogInformation("Todo with Id {Id} fetched successfully", request.Id);
            return todo.ToDto();

        }
    }
}

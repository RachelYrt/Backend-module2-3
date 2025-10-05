using MediatR;
using ToDoApi.DTOs;
using ToDoApi.Data;
using ToDoApi.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Queries
{

    public record  GetAllQuery(): IRequest<IEnumerable<TodoDto>>;

    public class GetAllQueryHandler: IRequestHandler<GetAllQuery,IEnumerable<TodoDto>>
    {
        private readonly TodoContext _context;
        private readonly ILogger<GetAllQueryHandler> _logger;
        public GetAllQueryHandler(TodoContext context,ILogger<GetAllQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<TodoDto>> Handle(GetAllQuery request,  CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all todos");
            var todos = await _context.Todos
                .Include(t => t.Category)
                .Select(t => t.ToDto())
                .ToListAsync(cancellationToken);
            _logger.LogInformation("Fetched {Count} todos successfully.", todos.Count);
            return todos;
        }

    }
}

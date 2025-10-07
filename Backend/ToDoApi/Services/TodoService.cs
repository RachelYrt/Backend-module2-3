using Microsoft.EntityFrameworkCore;
using ToDoApi.Mappers;
using ToDoApi.Data;
using ToDoApi.DTOs;
using ToDoApi.Queries;
using ToDoApi.Models;

public class TodoService
{
    private readonly TodoContext _context;
    private readonly ILogger<TodoService> _logger;
    public TodoService(TodoContext context, ILogger<TodoService> logger)
    {
        _context = context;
        _logger = logger;  
    }
    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        var todos = await _context.Todos
            .Include(t => t.Category)
            .ToListAsync();

        return todos.Select(t => t.ToDto());
    }
    public async Task<TodoDto?> GetByIdAsync(string id)
    {
        var todo = await _context.Todos
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (todo == null)
        {
            _logger.LogWarning("Todo with Id {Id} not found.", id);
            return null;
        }

        return todo.ToDto();
    }
    public async Task<TodoDto> CreateAsync(CreateTodoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Text))
            throw new ArgumentException("Todo text cannot be empty.");

        var todo = new Todo
        {
            Text = dto.Text,
            Description = dto.Description,
            CategoryId = dto.CategoryId
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Todo created successfully with Id: {Id}", todo.Id);
        return todo.ToDto();
    }

    public async Task<TodoDto?> UpdateAsync(string id, UpdateTodoDto dto)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            _logger.LogWarning("Todo with Id {Id} not found for update.", id);
            return null;
        }

        todo.Text = dto.Text;
        todo.Description = dto.Description;
        todo.CategoryId = dto.CategoryId;

        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Todo updated successfully with Id: {Id}", id);

        if (todo.CategoryId.HasValue)
            todo.Category = await _context.Categories.FindAsync(todo.CategoryId);

        return todo.ToDto();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            _logger.LogWarning("Todo with Id {Id} not found for deletion.", id);
            return false;
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Todo with Id {Id} deleted successfully.", id);
        return true;
    }


}
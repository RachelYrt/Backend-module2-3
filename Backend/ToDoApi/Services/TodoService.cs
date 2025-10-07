using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.DTOs;
using ToDoApi.Mappers;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class TodoService
    {
        private readonly TodoContext _context;
        public TodoService(TodoContext context) {
            _context = context;
        }
        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            return await _context.Todos
                .Include(t => t.Category).Select(t => t.ToDto()).ToArrayAsync();
        }
        //get single Todo by Id
        public async Task<TodoDto?> GetById(string id)
        {
            var todo = await _context.Todos
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t=>t.Id==id);
            return todo?.ToDto();
        }
        //create Todo task
        public async Task<TodoDto> CreateAsync(TodoDto dto)
        {
            var todo = dto.ToEntity();
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo.ToDto();
        }
        //update todo task
        public async Task<TodoDto> UpdateAsync(string id,TodoDto dto)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo is null)
                return null;
            todo.UpdateFromDto(dto);
            await _context.SaveChangesAsync();
            return todo.ToDto();  
        }
        //delete Todo
        public  async Task<bool> DeleteAsync(string id) {
            var todo = await _context.Todos.FindAsync(id);
            if (todo is null)
                return false;
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
               
        }


    }
}

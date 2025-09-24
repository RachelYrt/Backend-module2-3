using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos => Set<Todo>();
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}

    }
}

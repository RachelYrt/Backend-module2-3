using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos => Set<Todo>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>().HasData(
                new Todo
                {
                    Id="1",
                    Text="buy some candy.",
                    Description = "none",

                },
                new Todo
                {
                    Id = "5",
                    Text = "new meeting tomorrow.",
                    Description = "alart",

                },
                new Todo
                {
                    Id = "8",
                    Text = "new tennis class this week.",
                    Description = "low primary",
                }
                );
        }

    }
}

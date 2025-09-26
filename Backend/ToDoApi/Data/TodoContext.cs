using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<Category> Categories => Set<Category>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany(a => a.Todos)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Personal" },
                new Category { Id = 2, Name = "Work" },
                new Category { Id = 3, Name = "Travel" });
            modelBuilder.Entity<Todo>().HasData(
                new Todo
                {
                    Id="1",
                    Text="buy some candy.",
                    Description = "none",
                    CategoryId = 1

                },
                new Todo
                {
                    Id = "5",
                    Text = "new meeting tomorrow.",
                    Description = "alart",
                    CategoryId = 2

                },
                new Todo
                {
                    Id = "8",
                    Text = "new tennis class this week.",
                    Description = "low primary",
                    CategoryId = 1
                }
                );
        }

    }
}

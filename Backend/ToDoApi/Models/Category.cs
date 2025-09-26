namespace ToDoApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Todo>? Todos { get; set; }
    }
}

namespace ToDoApi.Models
{
    public class Todo
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string Text { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public int? CategoryId { get; set; }

    }
}

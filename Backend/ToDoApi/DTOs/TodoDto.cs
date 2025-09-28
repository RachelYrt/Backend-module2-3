using ToDoApi.Models;

namespace ToDoApi.DTOs
{
    public class TodoDto
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}

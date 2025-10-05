namespace ToDoApi.DTOs
{
    public class UpdateTodoDto
    {
        public string Text { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}

namespace ToDoApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? Discription { get; set; }

    }
}

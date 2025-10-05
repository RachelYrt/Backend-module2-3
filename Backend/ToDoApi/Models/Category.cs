using System.Text.Json.Serialization;

namespace ToDoApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}

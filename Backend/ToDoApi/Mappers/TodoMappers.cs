using ToDoApi.DTOs;
using ToDoApi.Models;

namespace ToDoApi.Mappers
{
    public static class TodoMappers
    {
        //if using automapper
        // 实体 -> DTO
        //CreateMap<Todo, TodoDto>()
        //        .ForMember(dest => dest.CategoryName,
        //                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

        //    // DTO -> 实体
        //    CreateMap<TodoDto, Todo>()
        //        .ForMember(dest => dest.Category, opt => opt.Ignore());
        // 避免 EF 循环插入 Category，通常只用 CategoryId

        public static TodoDto ToDto(this Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Text = todo.Text,
                Description = todo.Description,
                CategoryId = todo.CategoryId,
                CategoryName = todo.Category?.Name
            };
        }
        public static Todo ToEntity(this TodoDto dto)
        {
            return new Todo
            {
                //Id = string.IsNullOrEmpty(dto.Id)
                //? Guid.NewGuid().ToString(): dto.Id,
                Id = Guid.NewGuid().ToString(),
                Text = dto.Text,
                Description = dto.Description,
            };
        }
        public static void UpdateFromDto(this Todo todo, TodoDto dto)
        {
            todo.Text = dto.Text;
            todo.Description = dto.Description;
            if(dto.CategoryId.HasValue)
                todo.CategoryId = dto.CategoryId.Value;
        }
    }
}

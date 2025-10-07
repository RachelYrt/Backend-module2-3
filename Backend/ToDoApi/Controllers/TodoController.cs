using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.DTOs;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class TodoController:ControllerBase
    {
        //todo refactor this controller into another service layer
        //move all the query to the service, service should return DTO to controller
        //service should use depedency injection into controller
        private readonly TodoService _service; 
        public TodoController(TodoService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<TodoDto>> GetAll()
        {
            var todos = await _service.GetAllAsync();
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById([FromRoute]string id)
        {
            var todo = await _service.GetById(id);
            if (todo is null)
                return NotFound();
            return Ok(todo);

        }
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateNew([FromBody] TodoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),new {id = created.Id}, created);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> Update(string id, [FromBody]TodoDto updateDto)
        {
            var updated = await _service.UpdateAsync(id,updateDto);
            if(updated == null)
            {
                return NotFound();
            }
            
            return Ok(updated);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoDto>> DeleteTodo([FromRoute] string id)
        {
            var success = await _service.DeleteAsync(id);
            if(!success )
            {
                return NotFound();
            }
            
            return NoContent();

        }

    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Commands;
using ToDoApi.Data;
using ToDoApi.DTOs;
using ToDoApi.Models;
using ToDoApi.Queries;

namespace ToDoApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class TodoController:ControllerBase
    {
        //todo refactor this controller into another service layer
        //move all the query to the service, service should return DTO to controller
        //service should use depedency injection into controller
        
        //not use todoservice for CRUD
        private readonly IMediator _mediator;
        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll(CancellationToken ct)
        {
            var todos = await _mediator.Send(new GetAllQuery(),ct);
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById([FromRoute]string id,CancellationToken ct)
        {
            var todo = await _mediator.Send(new GetByIdQuery(id),ct);
            if (todo is null)
                return NotFound();
            return Ok(todo);

        }
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateNew([FromBody] CreateTodoCommand command, CancellationToken ct)
        {
            var created = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById),new {id = created.Id}, created);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> Update(string id, [FromBody]UpdateTodoCommand command, CancellationToken ct)
        {
            var updated = await _mediator.Send(command, ct);
            if(updated == null)
            {
                return NotFound();
            }
            
            return Ok(updated);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoDto>> DeleteTodo([FromRoute] string id,CancellationToken ct)
        {
            var success = await _mediator.Send(new DeleteTodoCommand(id),ct);
            if(!success )
            {
                return NotFound();
            }
            
            return NoContent();

        }

    }
}

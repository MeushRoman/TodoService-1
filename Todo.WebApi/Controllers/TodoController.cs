using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application.DTO;
using Todo.Application.Exceptions;
using Todo.Application.Services;
using Todo.Infrastructure.Abstractions;

namespace Todo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController: ControllerBase
    {
        private readonly ITodoContext _context;
        private readonly ITodoService _todoService;
        //private readonly ILogger _logger;

        public TodoController(ITodoContext context, ITodoService todoService/*, ILogger logger*/)
        {
            _context = context;
            _todoService = todoService;
            //_logger = logger;
        }

        /// <summary>
        /// Get a TodoItems Collection
        /// </summary>
        /// <returns>IEnumerable<TodoItemDTO></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodoItems() => CreatedAtAction(nameof(GetTodoItems), await _todoService.GetTodoItems());

        /// <summary>
        /// Get a TodoItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TodoItem</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodo(long id)
        {
            try
            {
                var todoItem = await _todoService.GetTodoItem(id);

                return CreatedAtAction(nameof(GetTodo), new { id = todoItem.Id }, todoItem);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "name": "Item #1",
        ///        "isComplete": false
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodo(CreatTodoItemDTO itemDTO)
        {
            var item = await _todoService.CreateTodoItem(itemDTO);

            return CreatedAtAction(nameof(CreateTodo), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoService.UpdateTodoItem(todoItemDTO);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            try
            {
                await _todoService.DeleteTodoItem(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }  
    }
}

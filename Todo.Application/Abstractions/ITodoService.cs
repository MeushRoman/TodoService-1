using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.DTO;

namespace Todo.Infrastructure.Abstractions
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO> GetTodoItem(long id);
        Task UpdateTodoItem(TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> CreateTodoItem(CreatTodoItemDTO todoItem);
        Task DeleteTodoItem(long id);
        Task<bool> TodoItemIsExists(long id);
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application.DTO;
using Todo.Application.Exceptions;
using Todo.Infrastructure.Abstractions;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoContext _context;
        private readonly IMapper _mapper;

        public TodoService(ITodoContext todoContext, IMapper mapper)
        {
            _context = todoContext;
            _mapper = mapper;
        }

        public async Task<TodoItemDTO> CreateTodoItem(CreatTodoItemDTO itemDTO)
        {
            var item = _mapper.Map<TodoItem>(itemDTO);

            _context.TodoItems.Add(item);

            await _context.SaveChangesAsync();

            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems
                                         .FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (todoItem is null)
            {
                throw new NotFoundException($"Не найдена запись с Id {id}. Невозможно выполнить удаление");
            }

            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync();
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(p => p.Id.Equals(id));

            if(todoItem is null)
            {
                throw new NotFoundException($"Не найдена запись с Id {id}");
            }

            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _context.TodoItems
                                         .FirstOrDefaultAsync(p => p.Id.Equals(todoItemDTO.Id));

            if(todoItem is null)
            {
                throw new NotFoundException($"Не найдена запись с Id {todoItemDTO.Id}. Невозможно обновить запись");
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems() =>
             await _context.TodoItems
                           .AsNoTracking()
                           .Select(x => _mapper.Map<TodoItemDTO>(x))
                           .ToListAsync();        

        public async Task<bool> TodoItemIsExists(long id) =>
           await _context.TodoItems
                         .AsNoTracking()
                         .AnyAsync(e => e.Id.Equals(id));
    }
}

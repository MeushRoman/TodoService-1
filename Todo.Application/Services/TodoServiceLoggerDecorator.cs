using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Todo.Application.DTO;
using Todo.Application.Exceptions;
using Todo.Infrastructure.Abstractions;

namespace Todo.Application.Services
{
    public class TodoServiceLoggerDecorator : ITodoService
    {
        private readonly ITodoService _todoService;
        private readonly ILogger _logger;

        public TodoServiceLoggerDecorator(ITodoService todoService, ILogger<TodoServiceLoggerDecorator> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        public async Task<TodoItemDTO> CreateTodoItem(CreatTodoItemDTO todoItem)
        {
            _logger.LogInformation($"Entering {MethodBase.GetCurrentMethod().DeclaringType.Name}.");

            try
            {
                return await _todoService.CreateTodoItem(todoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw ex;
            }
        }

        public async Task DeleteTodoItem(long id)
        {
            _logger.LogInformation($"Entering {MethodBase.GetCurrentMethod().DeclaringType.Name}.");

            try
            {
                await _todoService.DeleteTodoItem(id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            _logger.LogInformation($"Entering {MethodBase.GetCurrentMethod().DeclaringType.Name}.");

            try
            { 
                return await _todoService.GetTodoItem(id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            _logger.LogInformation($"Entering {MethodBase.GetCurrentMethod().DeclaringType.Name}.");

            try
            {
                return await _todoService.GetTodoItems();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
            
        }

        public async Task<bool> TodoItemIsExists(long id)
        {
            _logger.LogInformation($"Entering {MethodBase.GetCurrentMethod().DeclaringType.Name}.");

            try
            {
                return await _todoService.TodoItemIsExists(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
        }

        public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            _logger.LogInformation($"Entering {MethodBase.GetCurrentMethod().DeclaringType.Name}.");

            try
            {
                await _todoService.UpdateTodoItem(todoItemDTO);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception {0}", ex.Message);
                throw;
            }
        }
    }
}

using AutoMapper;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Application.DTO.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
            CreateMap<TodoItem, CreatTodoItemDTO>().ReverseMap();
        }
    }
}

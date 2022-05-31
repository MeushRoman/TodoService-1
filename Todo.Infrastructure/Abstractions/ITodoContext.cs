using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Infrastructure.Abstractions
{
    public interface ITodoContext
    {
        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync();
    }
}

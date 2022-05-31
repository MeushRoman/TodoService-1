using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Todo.Infrastructure.Abstractions;
using Todo.Infrastructure.Database.Entities;

namespace Todo.Infrastructure.Database.EF
{
    public class TodoContext : DbContext, ITodoContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
          : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}

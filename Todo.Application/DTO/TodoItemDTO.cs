using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Todo.Application.DTO
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}

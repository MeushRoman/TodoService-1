using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Todo.Application.DTO
{
    public class CreatTodoItemDTO
    {
        [Required]
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}

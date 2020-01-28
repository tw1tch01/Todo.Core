using Todo.Domain.Entities;
using Todo.Models.Mappings;

namespace Todo.Models.TodoNotes
{
    public class UpdateNoteDto : IMaps<TodoItemNote>
    {
        public string Comment { get; set; }
    }
}
using Todo.Domain.Entities;
using Todo.DomainModels.Mappings;

namespace Todo.DomainModels.TodoNotes
{
    public class UpdateNoteDto : IMaps<TodoItemNote>
    {
        public string Comment { get; set; }
    }
}
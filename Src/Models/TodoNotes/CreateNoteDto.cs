using System;
using Todo.Domain.Entities;
using Todo.Models.Mappings;

namespace Todo.Models.TodoNotes
{
    public class CreateNoteDto : IMaps<TodoItemNote>
    {
        public Guid ItemId { get; set; }

        public string Comment { get; set; }
    }
}
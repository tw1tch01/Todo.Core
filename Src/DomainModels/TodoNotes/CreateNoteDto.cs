using System;
using Todo.Domain.Entities;
using Todo.DomainModels.Mappings;

namespace Todo.DomainModels.TodoNotes
{
    public class CreateNoteDto : IMaps<TodoItemNote>
    {
        public Guid ItemId { get; set; }

        public string Comment { get; set; }
    }
}
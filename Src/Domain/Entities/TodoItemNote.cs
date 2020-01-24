using System;
using System.Collections.Generic;
using Todo.Domain.Common;

namespace Todo.Domain.Entities
{
    public class TodoItemNote : BaseEntity
    {
        public TodoItemNote()
        {
            Replies = new HashSet<TodoItemNote>();
        }

        public Guid NoteId { get; set; }
        public Guid ItemId { get; set; }
        public Guid? ParentNoteId { get; set; }
        public string Comment { get; set; }

        #region Navigational Properties

        public TodoItem Item { get; set; }
        public TodoItemNote ParentNote { get; set; }
        public ICollection<TodoItemNote> Replies { get; }

        #endregion Navigational Properties
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICollection<TodoItemNote> Replies { get; private set; }

        #endregion Navigational Properties

        #region Methods

        public static ICollection<TodoItemNote> GetParentNotes(ICollection<TodoItemNote> notes)
        {
            return notes.Where(n => !n.ParentNoteId.HasValue).ToList();
        }

        #endregion Methods
    }
}
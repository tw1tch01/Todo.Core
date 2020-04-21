using System;

namespace Todo.DomainModels.TodoNotes.Events.CreateNote
{
    public class BeforeNoteCreated
    {
        public BeforeNoteCreated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}
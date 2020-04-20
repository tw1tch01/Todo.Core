using System;

namespace Todo.Services.TodoNotes.Validation
{
    internal class ItemIdMismatchResult : NoteInvalidResult
    {
        private const string _message = "Reply's ItemId does not match the parent Note's ItemId.";
        private const string _parentNoteItemIdKey = "ParentNoteItemId";
        private const string _replyItemIdKey = "ReplyItemId";

        public ItemIdMismatchResult(Guid parentNoteItemId, Guid replyItemId)
            : base(Guid.Empty, _message)
        {
            Data[_parentNoteItemIdKey] = parentNoteItemId;
            Data[_replyItemIdKey] = replyItemId;
        }
    }
}
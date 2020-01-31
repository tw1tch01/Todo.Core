using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Services.TodoNotes.Specifications
{
    public class GetNotesByItemId : LinqSpecification<TodoItemNote>
    {
        private readonly Guid _itemId;

        public GetNotesByItemId(Guid itemId)
        {
            _itemId = itemId;
        }

        public override Expression<Func<TodoItemNote, bool>> AsExpression()
        {
            return note => note.ItemId == _itemId;
        }
    }
}
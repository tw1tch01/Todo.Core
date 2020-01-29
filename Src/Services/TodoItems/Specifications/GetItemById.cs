using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Specifications
{
    public class GetItemById : LinqSpecification<TodoItem>
    {
        public GetItemById(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.ItemId == ItemId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.TodoItems.Queries.Specifications
{
    internal class WithinItemIds : LinqSpecification<TodoItem>
    {
        private readonly ICollection<Guid> _itemIds;

        public WithinItemIds(ICollection<Guid> itemIds)
        {
            if (itemIds == null) throw new ArgumentNullException(nameof(itemIds));

            if (!itemIds.Any()) throw new ArgumentException("Value cannot be an empty collection.", nameof(itemIds));

            _itemIds = itemIds;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return entity => _itemIds.Contains(entity.ItemId);
        }
    }
}
﻿using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.TodoItems.Queries.Specifications
{
    internal class GetParentItems : LinqSpecification<TodoItem>
    {
        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => !item.ParentItemId.HasValue;
        }
    }
}
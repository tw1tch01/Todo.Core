using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Specifications
{
    public class SearchByName : LinqSpecification<TodoItem>
    {
        private readonly string _searchBy;

        public SearchByName(string searchBy)
        {
            if (string.IsNullOrWhiteSpace(searchBy)) throw new ArgumentException("Value cannot be null, empty or whitespace.", nameof(searchBy));

            _searchBy = searchBy;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => !string.IsNullOrWhiteSpace(item.Name) && item.Name.Contains(_searchBy);
        }
    }
}
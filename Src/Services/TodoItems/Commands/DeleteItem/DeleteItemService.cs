using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.DeleteItem
{
    internal class DeleteItemService : IDeleteItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;

        public DeleteItemService(IContextRepository<ITodoContext> repository)
        {
            _repository = repository;
        }

        public async Task DeleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            _repository.Remove(item);
            await _repository.SaveAsync();
        }
    }
}
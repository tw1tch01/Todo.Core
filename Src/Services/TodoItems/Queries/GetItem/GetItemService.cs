using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Queries.GetItem
{
    internal class GetItemService : IGetItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;

        public GetItemService(IContextRepository<ITodoContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TodoItemDetails> GetItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            var getChildItemsTask = _repository.ListAsync(new GetItemsByParentId(item.ItemId));

            // TODO: get notes:
            //var getNotesTask = _repository.ListAsync(new GetNotesByItemId(item.ItemId));

            await Task.WhenAll(getChildItemsTask/*, getNotesTask*/);

            item.ChildItems.ToList().AddRange(await getChildItemsTask);
            //item.Notes.ToList().AddRange(await getNotesTask);

            var details = _mapper.Map<TodoItemDetails>(item);

            return details;
        }
    }
}
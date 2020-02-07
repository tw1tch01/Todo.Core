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
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoItems.Queries.GetItem
{
    public class GetItemService : IGetItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;

        public GetItemService(IContextRepository<ITodoContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TodoItemDetails> GetItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            var getChildItemsTask = _repository.ListAsync(new GetItemsByParentId(item.ItemId));
            var getNotesTask = _repository.ListAsync(new GetNotesByItemId(item.ItemId).Include(n => n.Replies));

            await Task.WhenAll(getChildItemsTask, getNotesTask);

            item.ChildItems.ToList().AddRange(await getChildItemsTask);
            item.Notes.ToList().AddRange(await getNotesTask);

            var details = _mapper.Map<TodoItemDetails>(item);

            return details;
        }
    }
}
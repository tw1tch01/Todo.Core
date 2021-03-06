﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
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

            if (item == null) return null;

            var getChildItems = await _repository.ListAsync(new GetItemsByParentId(item.ItemId));
            var getNotes = await _repository.ListAsync(new GetNotesByItemId(item.ItemId).Include(n => n.Replies));

            item.GroupNotesWithReplies();

            var details = _mapper.Map<TodoItemDetails>(item);

            return details;
        }
    }
}
﻿using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.Actions.StartItem
{
    internal class StartItemService : IStartItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;

        public StartItemService(IContextRepository<ITodoContext> repository)
        {
            _repository = repository;
        }

        public async Task StartItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            item.StartItem();
        }
    }
}
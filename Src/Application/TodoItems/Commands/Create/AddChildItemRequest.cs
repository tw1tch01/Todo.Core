using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

namespace Todo.Application.TodoItems.Commands.Create
{
    internal class AddChildItemRequest : IRequest<Guid>
    {
        public AddChildItemRequest(Guid parentId, CreateItemDto childItemDto)
        {
            ParentId = parentId;
            ChildItemDto = childItemDto;
        }

        internal Guid ParentId { get; }

        internal CreateItemDto ChildItemDto { get; }

        internal class RequestHandler : IRequestHandler<AddChildItemRequest, Guid>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(AddChildItemRequest request, CancellationToken cancellationToken)
            {
                var parentItem = await _repository.FindByPrimaryKeyAsync<TodoItem, Guid>(request.ParentId);

                if (parentItem == null) throw new NotFoundException(nameof(TodoItem), request.ParentId);

                var childItem = _mapper.Map<TodoItem>(request.ChildItemDto);
                childItem.ParentItemId = parentItem.ItemId;

                await _repository.AddAsync(childItem);
                await _repository.SaveAsync();

                return childItem.ItemId;
            }
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;

namespace Todo.Application.TodoItems.Commands.Update
{
    internal class UpdateItemRequest : IRequest
    {
        public UpdateItemRequest(Guid itemId, UpdateItemDto itemDto)
        {
            ItemId = itemId;
            ItemDto = itemDto ?? throw new ArgumentNullException(nameof(ItemDto));
        }

        internal Guid ItemId { get; }
        internal UpdateItemDto ItemDto { get; }

        internal class RequestHandler : IRequestHandler<UpdateItemRequest>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
            {
                var item = await _repository.FindByPrimaryKeyAsync<TodoItem, Guid>(request.ItemId);

                if (item == null) throw new NotFoundException(nameof(TodoItem), request.ItemId);

                _mapper.Map(request.ItemDto, item);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
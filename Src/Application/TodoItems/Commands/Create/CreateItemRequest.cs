using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

namespace Todo.Application.TodoItems.Commands.Create
{
    internal class CreateItemRequest : IRequest<Guid>
    {
        public CreateItemRequest(CreateItemDto itemDto)
        {
            ItemDto = itemDto ?? throw new ArgumentNullException(nameof(itemDto));
        }

        internal CreateItemDto ItemDto { get; }

        internal class RequestHandler : IRequestHandler<CreateItemRequest, Guid>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(CreateItemRequest request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<TodoItem>(request.ItemDto);

                await _repository.AddAsync(item);
                await _repository.SaveAsync();

                return item.ItemId;
            }
        }
    }
}
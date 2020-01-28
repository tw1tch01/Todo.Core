using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Models.TodoNotes;

namespace Todo.Application.TodoNotes.Commands.Create
{
    internal class CreateNoteRequest : IRequest<Guid>
    {
        public CreateNoteRequest(Guid itemId, CreateNoteDto noteDto)
        {
            Specification = new GetItemById(itemId);
            NoteDto = noteDto;
        }

        internal GetItemById Specification { get; }

        internal CreateNoteDto NoteDto { get; }

        internal class RequestHandler : IRequestHandler<CreateNoteRequest, Guid>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
            {
                var item = await _repository.GetAsync(request.Specification);

                if (item == null) throw new NotFoundException(nameof(TodoItem), request.Specification.ItemId);

                var note = _mapper.Map<TodoItemNote>(request.NoteDto);

                item.Notes.Add(note);

                await _repository.SaveAsync();

                return note.NoteId;
            }
        }
    }
}
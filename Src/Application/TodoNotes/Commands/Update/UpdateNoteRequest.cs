using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoNotes.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Models.TodoNotes;

namespace Todo.Application.TodoNotes.Commands.Update
{
    internal class UpdateNoteRequest : IRequest
    {
        public UpdateNoteRequest(Guid noteId, UpdateNoteDto noteDto)
        {
            Specification = new GetNoteById(noteId);
            NoteDto = noteDto;
        }

        internal GetNoteById Specification { get; }

        internal UpdateNoteDto NoteDto { get; }

        internal class RequestHandler : IRequestHandler<UpdateNoteRequest>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
            {
                var note = await _repository.GetAsync(request.Specification);

                if (note == null) throw new NotFoundException(nameof(TodoItemNote), request.Specification.NoteId);

                _mapper.Map(request.NoteDto, note);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
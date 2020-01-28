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

namespace Todo.Application.TodoNotes.Commands.Create
{
    internal class ReplyOnNoteRequest : IRequest<Guid>
    {
        public ReplyOnNoteRequest(Guid noteId, CreateNoteDto noteDto)
        {
            Specification = new GetNoteById(noteId);
            NoteDto = noteDto;
        }

        internal GetNoteById Specification { get; }

        internal CreateNoteDto NoteDto { get; }

        internal class RequestHandler : IRequestHandler<ReplyOnNoteRequest, Guid>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(ReplyOnNoteRequest request, CancellationToken cancellationToken)
            {
                var note = await _repository.GetAsync(request.Specification);

                if (note == null) throw new NotFoundException(nameof(TodoItem), request.Specification.NoteId);

                var reply = _mapper.Map<TodoItemNote>(request.NoteDto);

                note.Replies.Add(reply);
                await _repository.SaveAsync();

                return reply.NoteId;
            }
        }
    }
}
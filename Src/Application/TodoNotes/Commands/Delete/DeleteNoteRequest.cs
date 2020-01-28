using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoNotes.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;

namespace Todo.Application.TodoNotes.Commands.Delete
{
    internal class DeleteNoteRequest : IRequest
    {
        public DeleteNoteRequest(Guid noteId)
        {
            Specification = new GetNoteById(noteId);
        }

        internal GetNoteById Specification { get; }

        internal class RequestHandler : IRequestHandler<DeleteNoteRequest>
        {
            private readonly IContextRepository<ITodoContext> _repository;

            public RequestHandler(IContextRepository<ITodoContext> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
            {
                var note = await _repository.GetAsync(request.Specification);

                if (note == null) throw new NotFoundException(nameof(TodoItemNote), request.Specification.NoteId);

                _repository.Remove(note);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Events.TodoNotes;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.UpdateNote
{
    internal class UpdateNoteService : IUpdateNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateNoteService(IContextRepository<ITodoContext> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task UpdateNote(Guid noteId, UpdateNoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            var note = await _repository.GetAsync(new GetNoteById(noteId));

            if (note == null) throw new NotFoundException(nameof(TodoItemNote), noteId);

            _mapper.Map(noteDto, note);

            await _repository.SaveAsync();

            await _mediator.Publish(new NoteWasUpdated(note.NoteId));
        }
    }
}
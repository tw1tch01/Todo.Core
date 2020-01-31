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
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.CreateNote
{
    internal class CreateNoteService : ICreateNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateNoteService(IContextRepository<ITodoContext> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            var note = _mapper.Map<TodoItemNote>(noteDto);

            item.Notes.Add(note);
            await _repository.SaveAsync();

            await _mediator.Publish(new NoteWasCreated(note.NoteId));

            return note.NoteId;
        }

        public async Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto childNoteDto)
        {
            if (childNoteDto == null) throw new ArgumentNullException(nameof(childNoteDto));

            var parentNote = await _repository.GetAsync(new GetNoteById(parentNoteId));

            if (parentNote == null) throw new NotFoundException(nameof(TodoItem), parentNoteId);

            var reply = _mapper.Map<TodoItemNote>(childNoteDto);

            parentNote.Replies.Add(reply);
            await _repository.SaveAsync();

            await _mediator.Publish(new ReplyWasCreated(parentNoteId, reply.NoteId));

            return reply.NoteId;
        }
    }
}
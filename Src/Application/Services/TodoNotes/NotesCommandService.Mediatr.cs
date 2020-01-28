using System;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Interfaces.TodoNotes;
using Todo.Application.TodoNotes.Commands.Create;
using Todo.Application.TodoNotes.Commands.Delete;
using Todo.Application.TodoNotes.Commands.Update;
using Todo.DomainModels.TodoNotes;

namespace Todo.Application.Services.TodoNotes
{
    public class NotesCommandService : INotesCommandService
    {
        private readonly IMediator _mediator;

        public NotesCommandService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto)
        {
            return await _mediator.Send(new CreateNoteRequest(itemId, noteDto));
        }

        public async Task DeleteNote(Guid noteId)
        {
            await _mediator.Send(new DeleteNoteRequest(noteId));
        }

        public async Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto noteDto)
        {
            return await _mediator.Send(new ReplyOnNoteRequest(parentNoteId, noteDto));
        }

        public async Task UpdateNote(Guid noteId, UpdateNoteDto noteDto)
        {
            await _mediator.Send(new UpdateNoteRequest(noteId, noteDto));
        }
    }
}
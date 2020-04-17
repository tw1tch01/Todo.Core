using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Commands.UpdateNote;

namespace Todo.Application.Services.TodoNotes
{
    public class NotesCommandService : ICreateNoteService, IDeleteNoteService, IUpdateNoteService
    {
        private readonly ICreateNoteService _createNoteService;
        private readonly IDeleteNoteService _deleteNoteService;
        private readonly IUpdateNoteService _updateNoteService;

        public NotesCommandService
        (
            ICreateNoteService createNoteService,
            IDeleteNoteService deleteNoteService,
            IUpdateNoteService updateNoteService
        )
        {
            _createNoteService = createNoteService;
            _deleteNoteService = deleteNoteService;
            _updateNoteService = updateNoteService;
        }

        public Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto) => _createNoteService.CreateNote(itemId, noteDto);

        public Task DeleteNote(Guid noteId) => _deleteNoteService.DeleteNote(noteId);

        public Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto noteDto) => _createNoteService.ReplyOnNote(parentNoteId, noteDto);

        public Task UpdateNote(Guid noteId, UpdateNoteDto noteDto) => _updateNoteService.UpdateNote(noteId, noteDto);
    }
}
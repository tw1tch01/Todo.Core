using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Commands.UpdateNote;

namespace Todo.Application.Services.TodoNotes.NoteCommands
{
    public class NotesCommandService : INotesCommandService
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

        public async Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto)
        {
            return await _createNoteService.CreateNote(itemId, noteDto);
        }

        public async Task DeleteNote(Guid noteId)
        {
            await _deleteNoteService.DeleteNote(noteId);
        }

        public async Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto noteDto)
        {
            return await _createNoteService.ReplyOnNote(parentNoteId, noteDto);
        }

        public async Task UpdateNote(Guid noteId, UpdateNoteDto noteDto)
        {
            await _updateNoteService.UpdateNote(noteId, noteDto);
        }
    }
}
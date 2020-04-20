using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Commands.UpdateNote;
using Todo.Services.TodoNotes.Validation;

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

        public Task<NoteValidationResult> CreateNote(CreateNoteDto noteDto) => _createNoteService.CreateNote(noteDto);

        public Task<NoteValidationResult> DeleteNote(Guid noteId) => _deleteNoteService.DeleteNote(noteId);

        public Task<NoteValidationResult> ReplyOnNote(Guid parentNoteId, CreateNoteDto replyDto) => _createNoteService.ReplyOnNote(parentNoteId, replyDto);

        public Task<NoteValidationResult> UpdateNote(Guid noteId, UpdateNoteDto noteDto) => _updateNoteService.UpdateNote(noteId, noteDto);
    }
}
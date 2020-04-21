using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;
using Todo.Services.TodoNotes.Validation;

namespace Todo.Services.TodoNotes.Commands.CreateNote
{
    public interface ICreateNoteService
    {
        Task<NoteValidationResult> CreateNote(CreateNoteDto noteDto);

        Task<NoteValidationResult> ReplyOnNote(Guid parentNoteId, CreateNoteDto childNoteDto);
    }
}
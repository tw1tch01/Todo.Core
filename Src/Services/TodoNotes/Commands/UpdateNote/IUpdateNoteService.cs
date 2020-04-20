using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;
using Todo.Services.TodoNotes.Validation;

namespace Todo.Services.TodoNotes.Commands.UpdateNote
{
    public interface IUpdateNoteService
    {
        Task<NoteValidationResult> UpdateNote(Guid noteId, UpdateNoteDto noteDto);
    }
}
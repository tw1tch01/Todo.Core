using System;
using System.Threading.Tasks;
using Todo.Services.TodoNotes.Validation;

namespace Todo.Services.TodoNotes.Commands.DeleteNote
{
    public interface IDeleteNoteService
    {
        Task<NoteValidationResult> DeleteNote(Guid noteId);
    }
}
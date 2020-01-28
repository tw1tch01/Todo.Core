using System;
using System.Threading.Tasks;
using Todo.Models.TodoNotes;

namespace Todo.Application.Interfaces.TodoNotes
{
    public interface INotesCommandService
    {
        Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto);

        Task DeleteNote(Guid noteId);

        Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto noteDto);

        Task UpdateNote(Guid noteId, UpdateNoteDto noteDto);
    }
}
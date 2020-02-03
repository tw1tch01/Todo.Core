using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoNotes.Commands.DeleteNote
{
    public interface IDeleteNoteService
    {
        Task DeleteNote(Guid noteId);
    }
}
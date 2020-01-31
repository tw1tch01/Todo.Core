using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services.TodoNotes.Commands.DeleteNote
{
    public interface IDeleteNoteService
    {
        Task DeleteNote(Guid noteId);
    }
}

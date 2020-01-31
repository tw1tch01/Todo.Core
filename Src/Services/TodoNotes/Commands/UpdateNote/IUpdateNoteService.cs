using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;

namespace Todo.Services.TodoNotes.Commands.UpdateNote
{
    public interface IUpdateNoteService
    {
        Task UpdateNote(Guid noteId, UpdateNoteDto noteDto);
    }
}
using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoNotes;

namespace Todo.Services.TodoNotes.Commands.CreateNote
{
    public interface ICreateNoteService
    {
        Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto);

        Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto childNoteDto);
    }
}
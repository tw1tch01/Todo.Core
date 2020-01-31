using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Commands.UpdateNote;

namespace Todo.Application.Services.TodoNotes.NoteCommands
{
    public interface INotesCommandService : ICreateNoteService,
                                            IDeleteNoteService,
                                            IUpdateNoteService
    {
    }
}
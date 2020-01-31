using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.DeleteNote
{
    internal class DeleteNoteService : IDeleteNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;

        public DeleteNoteService(IContextRepository<ITodoContext> repository)
        {
            _repository = repository;
        }

        public async Task DeleteNote(Guid noteId)
        {
            var note = await _repository.GetAsync(new GetNoteById(noteId));

            if (note == null) throw new NotFoundException(nameof(TodoItemNote), noteId);

            _repository.Remove(note);

            await _repository.SaveAsync();
        }
    }
}
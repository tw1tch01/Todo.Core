using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.CreateNote
{
    internal class CreateNoteService : ICreateNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;

        public CreateNoteService(IContextRepository<ITodoContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            var note = _mapper.Map<TodoItemNote>(noteDto);

            item.Notes.Add(note);

            await _repository.SaveAsync();

            return note.NoteId;
        }

        public async Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto childNoteDto)
        {
            var note = await _repository.GetAsync(new GetNoteById(parentNoteId));

            if (note == null) throw new NotFoundException(nameof(TodoItem), parentNoteId);

            var reply = _mapper.Map<TodoItemNote>(childNoteDto);

            note.Replies.Add(reply);

            await _repository.SaveAsync();

            return reply.NoteId;
        }
    }
}
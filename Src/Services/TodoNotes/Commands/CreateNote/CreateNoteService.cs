using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Events.TodoNotes.CreateNote;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.CreateNote
{
    internal class CreateNoteService : ICreateNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public CreateNoteService(IContextRepository<ITodoContext> repository, IMapper mapper, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public async Task<Guid> CreateNote(Guid itemId, CreateNoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeNoteCreatedProcess(itemId));

            var note = _mapper.Map<TodoItemNote>(noteDto);

            item.Notes.Add(note);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new NotedCreatedProcess(note.NoteId));
            var notification = _notificationService.Queue(new NotedCreatedNotification(note.NoteId));

            await Task.WhenAll(notification, workflow);

            return note.NoteId;
        }

        public async Task<Guid> ReplyOnNote(Guid parentNoteId, CreateNoteDto childNoteDto)
        {
            if (childNoteDto == null) throw new ArgumentNullException(nameof(childNoteDto));

            var parentNote = await _repository.GetAsync(new GetNoteById(parentNoteId));

            if (parentNote == null) throw new NotFoundException(nameof(TodoItem), parentNoteId);

            await _workflowService.Process(new BeforeReplyCreatedProcess(parentNote.NoteId));

            var reply = _mapper.Map<TodoItemNote>(childNoteDto);

            parentNote.Replies.Add(reply);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ReplyCreatedProcess(parentNote.NoteId, reply.NoteId));
            var notification = _notificationService.Queue(new ReplyCreatedNotification(parentNote.NoteId, reply.NoteId));

            await Task.WhenAll(notification, workflow);

            return reply.NoteId;
        }
    }
}
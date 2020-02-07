using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoNotes.Events.UpdateNote;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.UpdateNote
{
    internal class UpdateNoteService : IUpdateNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public UpdateNoteService(IContextRepository<ITodoContext> repository, IMapper mapper, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public async Task UpdateNote(Guid noteId, UpdateNoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            var note = await _repository.GetAsync(new GetNoteById(noteId));

            if (note == null) throw new NotFoundException(nameof(TodoItemNote), noteId);

            await _workflowService.Process(new BeforeNoteUpdatedProcess(noteId));

            _mapper.Map(noteDto, note);

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new NoteUpdatedProcess(note.NoteId));
            var notification = _notificationService.Queue(new NoteUpdatedNotification(note.NoteId));

            await Task.WhenAll(notification, workflow);
        }
    }
}
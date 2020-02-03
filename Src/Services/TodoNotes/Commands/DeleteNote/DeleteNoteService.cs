using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Events.TodoNotes.DeleteNote;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.TodoNotes.Commands.DeleteNote
{
    internal class DeleteNoteService : IDeleteNoteService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public DeleteNoteService(IContextRepository<ITodoContext> repository, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public async Task DeleteNote(Guid noteId)
        {
            var note = await _repository.GetAsync(new GetNoteById(noteId));

            if (note == null) throw new NotFoundException(nameof(TodoItemNote), noteId);

            await _workflowService.Process(new BeforeNoteDeletedProcess(noteId));

            _repository.Remove(note);

            await _repository.SaveAsync();

            var deletedOn = DateTime.UtcNow;
            var workflow = _workflowService.Process(new NoteDeletedProcess(noteId, deletedOn));
            var notification = _notificationService.Queue(new NoteDeletedNotification(noteId, deletedOn));

            await Task.WhenAll(notification, workflow);
        }
    }
}
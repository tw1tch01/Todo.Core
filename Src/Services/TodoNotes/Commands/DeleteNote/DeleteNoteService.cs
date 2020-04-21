using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.TodoNotes.Events.DeleteNote;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoNotes.Specifications;
using Todo.Services.TodoNotes.Validation;

namespace Todo.Services.TodoNotes.Commands.DeleteNote
{
    public class DeleteNoteService : IDeleteNoteService
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

        public virtual async Task<NoteValidationResult> DeleteNote(Guid noteId)
        {
            var note = await _repository.GetAsync(new GetNoteById(noteId));

            if (note == null) return new NoteNotFoundResult(noteId);

            await _workflowService.Process(new BeforeNoteDeletedProcess(noteId));

            _repository.Remove(note);

            await _repository.SaveAsync();

            var deletedOn = DateTime.UtcNow;
            var workflow = _workflowService.Process(new NoteDeletedProcess(noteId, deletedOn));
            var notification = _notificationService.Queue(new NoteDeletedNotification(noteId, deletedOn));

            await Task.WhenAll(notification, workflow);

            return new NoteDeletedResult(noteId, deletedOn);
        }
    }
}
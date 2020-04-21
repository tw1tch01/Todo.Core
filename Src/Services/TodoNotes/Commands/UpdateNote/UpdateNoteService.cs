using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using FluentValidation.Results;
using Todo.DomainModels.TodoNotes;
using Todo.DomainModels.TodoNotes.Validators;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoNotes.Events.UpdateNote;
using Todo.Services.TodoNotes.Specifications;
using Todo.Services.TodoNotes.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Commands.UpdateNote
{
    public class UpdateNoteService : IUpdateNoteService
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

        public virtual async Task<NoteValidationResult> UpdateNote(Guid noteId, UpdateNoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            var validationResult = ValidateDto(noteDto);

            if (!validationResult.IsValid) return new InvalidDtoResult(validationResult.Errors);

            var note = await _repository.GetAsync(new GetNoteById(noteId));

            if (note == null) return new NoteNotFoundResult(noteId);

            await _workflowService.Process(new BeforeNoteUpdatedProcess(noteId));

            _mapper.Map(noteDto, note);

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new NoteUpdatedProcess(note.NoteId));
            var notification = _notificationService.Queue(new NoteUpdatedNotification(note.NoteId));

            await Task.WhenAll(notification, workflow);

            return new NoteUpdatedResult(note.NoteId, note.ModifiedOn.Value);
        }

        #region Private Methods

        private ValidationResult ValidateDto(UpdateNoteDto noteDto)
        {
            var validator = new UpdateNoteValidator();
            var result = validator.Validate(noteDto);
            return result;
        }

        #endregion Private Methods
    }
}
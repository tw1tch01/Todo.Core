using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using FluentValidation.Results;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;
using Todo.DomainModels.TodoNotes.Validators;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoNotes.Events.CreateNote;
using Todo.Services.TodoNotes.Specifications;
using Todo.Services.TodoNotes.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Commands.CreateNote
{
    public class CreateNoteService : ICreateNoteService
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

        public virtual async Task<NoteValidationResult> CreateNote(CreateNoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            var validationResult = ValidateDto(noteDto);

            if (!validationResult.IsValid) return new InvalidDtoResult(validationResult.Errors);

            var item = await _repository.GetAsync(new GetItemById(noteDto.ItemId));

            if (item == null) return new ItemNotFoundResult(noteDto.ItemId);

            await _workflowService.Process(new BeforeNoteCreatedProcess(noteDto.ItemId));

            var note = _mapper.Map<TodoItemNote>(noteDto);

            item.Notes.Add(note);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new NotedCreatedProcess(note.NoteId));
            var notification = _notificationService.Queue(new NotedCreatedNotification(note.NoteId));

            await Task.WhenAll(notification, workflow);

            return new NoteCreatedResult(note.NoteId, note.CreatedOn);
        }

        public async Task<NoteValidationResult> ReplyOnNote(Guid parentNoteId, CreateNoteDto replyDto)
        {
            if (replyDto == null) throw new ArgumentNullException(nameof(replyDto));

            var validationResult = ValidateDto(replyDto);

            if (!validationResult.IsValid) return new InvalidDtoResult(validationResult.Errors);

            var parentNote = await _repository.GetAsync(new GetNoteById(parentNoteId));

            if (parentNote == null) return new NoteNotFoundResult(parentNoteId);

            if (parentNote.ItemId != replyDto.ItemId) return new ItemIdMismatchResult(parentNote.ItemId, replyDto.ItemId);

            await _workflowService.Process(new BeforeReplyCreatedProcess(parentNote.NoteId));

            var reply = _mapper.Map<TodoItemNote>(replyDto);

            parentNote.Replies.Add(reply);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ReplyCreatedProcess(parentNote.NoteId, reply.NoteId));
            var notification = _notificationService.Queue(new ReplyCreatedNotification(parentNote.NoteId, reply.NoteId));

            await Task.WhenAll(notification, workflow);

            return new NoteCreatedResult(reply.NoteId, reply.CreatedOn);
        }

        #region Private Methods

        private ValidationResult ValidateDto(CreateNoteDto noteDto)
        {
            var validator = new CreateNoteValidator();
            var result = validator.Validate(noteDto);
            return result;
        }

        #endregion Private Methods
    }
}
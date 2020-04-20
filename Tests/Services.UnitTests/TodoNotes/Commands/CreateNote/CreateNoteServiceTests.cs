using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Specifications;
using Todo.Services.TodoNotes.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoNotes.Commands.CreateNote
{
    [TestFixture]
    public class CreateNoteServiceTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Test]
        public void CreateNote_WhenNoteDtoIsNull_ThrowsArgumentNullException()
        {
            CreateNoteDto noteDto = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.CreateNote(noteDto));
        }

        [Test]
        public async Task CreateNote_WhenCreateNoteDtoIsInvalid_ReturnsInvalidDtoResult()
        {
            var noteDto = new CreateNoteDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CreateNote(noteDto);

            Assert.IsInstanceOf<InvalidDtoResult>(result);
        }

        [Test]
        public async Task CreateNote_WhenItemDoesNotExist_ReturnsItemNotFoundResult()
        {
            var noteDto = new CreateNoteDto
            {
                ItemId = Guid.NewGuid(),
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == noteDto.ItemId))).ReturnsAsync(() => null);
            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CreateNote(noteDto);

            Assert.IsInstanceOf<ItemNotFoundResult>(result);
        }

        [Test]
        public async Task CreateNote_WhenItemDoesExist_ReturnsNoteCreatedResult()
        {
            var noteDto = new CreateNoteDto
            {
                ItemId = Guid.NewGuid(),
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == noteDto.ItemId))).ReturnsAsync(() => new TodoItem { ItemId = noteDto.ItemId });
            mockMapper.Setup(m => m.Map<TodoItemNote>(It.IsAny<CreateNoteDto>())).Returns(new TodoItemNote { CreatedOn = DateTime.UtcNow });
            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CreateNote(noteDto);

            Assert.IsInstanceOf<NoteCreatedResult>(result);
        }

        [Test]
        public void ReplyOnNote_WhenChildNoteDtoIsNull_ThrowsArgumentNullException()
        {
            CreateNoteDto childNoteDto = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.ReplyOnNote(Guid.NewGuid(), childNoteDto));
        }

        [Test]
        public async Task ReplyOnNote_WhenChildNoteDtoIsInvalid_ReturnsInvalidDtoResult()
        {
            var childNoteDto = new CreateNoteDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.ReplyOnNote(Guid.NewGuid(), childNoteDto);

            Assert.IsInstanceOf<InvalidDtoResult>(result); ;
        }

        [Test]
        public async Task ReplyOnNote_WhenParentNoteDoesNotExist_ReturnsNoteNotFoundResult()
        {
            var noteDto = new CreateNoteDto
            {
                ItemId = Guid.NewGuid(),
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetNoteById>())).ReturnsAsync(() => null);

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.ReplyOnNote(Guid.NewGuid(), noteDto);

            Assert.IsInstanceOf<NoteNotFoundResult>(result);
        }

        [Test]
        public async Task ReplyOnNote_WhenParentNoteItemIdDoesNotMatchDtoItemId_ReturnsItemIdMismatchResult()
        {
            var parentNote = new TodoItemNote
            {
                NoteId = Guid.NewGuid(),
                ItemId = Guid.NewGuid(),
                Comment = _fixture.Create<string>()
            };
            var noteDto = new CreateNoteDto
            {
                ItemId = Guid.NewGuid(),
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetNoteById>())).ReturnsAsync(() => parentNote);

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.ReplyOnNote(parentNote.NoteId, noteDto);

            Assert.IsInstanceOf<ItemIdMismatchResult>(result);
        }

        [Test]
        public async Task ReplyOnNote_WhenParentNoteExistsAndItemIdsMatch_ReturnsNoteCreatedResult()
        {
            var parentNote = new TodoItemNote
            {
                NoteId = Guid.NewGuid(),
                ItemId = Guid.NewGuid(),
                Comment = _fixture.Create<string>()
            };
            var noteDto = new CreateNoteDto
            {
                ItemId = parentNote.ItemId,
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetNoteById>())).ReturnsAsync(() => parentNote);
            mockMapper.Setup(m => m.Map<TodoItemNote>(It.IsAny<CreateNoteDto>())).Returns(new TodoItemNote { CreatedOn = DateTime.UtcNow });
            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.ReplyOnNote(parentNote.NoteId, noteDto);

            Assert.IsInstanceOf<NoteCreatedResult>(result);
        }
    }
}
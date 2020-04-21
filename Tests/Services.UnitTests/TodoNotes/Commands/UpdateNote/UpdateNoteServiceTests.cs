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
using Todo.Services.TodoNotes.Commands.UpdateNote;
using Todo.Services.TodoNotes.Specifications;
using Todo.Services.TodoNotes.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoNotes.Commands.UpdateNote
{
    [TestFixture]
    public class UpdateNoteServiceTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Test]
        public void UpdateNote_WhenUpdateNoteDtoIsNull_ThrowsArgumentNullException()
        {
            UpdateNoteDto noteDto = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new UpdateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateNote(Guid.NewGuid(), noteDto));
        }

        [Test]
        public async Task UpdateNote_WhenNoteDoesNotExist_ReturnsNoteNotFoundResult()
        {
            var noteId = Guid.NewGuid();
            var noteDto = new UpdateNoteDto
            {
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetNoteById>(a => a.NoteId == noteId))).ReturnsAsync(() => null);

            var service = new UpdateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.UpdateNote(noteId, noteDto);

            Assert.IsInstanceOf<NoteNotFoundResult>(result);
        }

        [Test]
        public async Task UpdateNote_WhenNoteDoesExist_ReturnsNoteUpdatedResult()
        {
            var noteId = Guid.NewGuid();
            var note = new TodoItemNote
            {
                ModifiedOn = DateTime.UtcNow
            };
            var noteDto = new UpdateNoteDto
            {
                Comment = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetNoteById>(a => a.NoteId == noteId))).ReturnsAsync(() => note);
            mockMapper.Setup(m => m.Map(It.IsAny<UpdateNoteDto>(), It.IsAny<TodoItemNote>())).Returns(note);
            var service = new UpdateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.UpdateNote(noteId, noteDto);

            Assert.IsInstanceOf<NoteUpdatedResult>(result);
        }
    }
}
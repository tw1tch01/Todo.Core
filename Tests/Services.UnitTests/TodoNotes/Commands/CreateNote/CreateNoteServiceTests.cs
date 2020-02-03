using System;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.UnitTests.TodoNotes.Commands.CreateNote
{
    [TestFixture]
    public class CreateNoteServiceTests
    {
        [Test]
        public void CreateNote_WhenNoteDtoIsNull_ThrowsArgumentNullException()
        {
            CreateNoteDto noteDto = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateNote(Guid.NewGuid(), noteDto));
        }

        [Test]
        public void CreateNote_WhenItemDoesNotExist_ThrowsNotFoundException()
        {
            var itemId = Guid.NewGuid();
            var noteDto = new CreateNoteDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == itemId))).ReturnsAsync(() => null);

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.CreateNote(itemId, noteDto));
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
        public void ReplyOnNote_WhenParentNoteDoesNotExist_ThrowsNotFoundException()
        {
            var noteId = Guid.NewGuid();
            var noteDto = new CreateNoteDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.Is<GetNoteById>(a => a.NoteId == noteId))).ReturnsAsync(() => null);

            var service = new CreateNoteService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.ReplyOnNote(noteId, noteDto));
        }
    }
}
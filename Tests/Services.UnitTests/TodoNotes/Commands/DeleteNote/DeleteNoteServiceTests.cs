using System;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Specifications;
using Todo.Services.TodoNotes.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoNotes.Commands.DeleteNote
{
    [TestFixture]
    public class DeleteNoteServiceTests
    {
        [Test]
        public async Task DeleteNote_WhenNoteDoesNotExist_ReturnsNoteNotFoundResult()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            var service = new DeleteNoteService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.DeleteNote(Guid.NewGuid());

            Assert.IsInstanceOf<NoteNotFoundResult>(result);
        }

        [Test]
        public async Task DeleteNote_WhenNoteDoesExist_ReturnsNoteDeletedResult()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetNoteById>())).ReturnsAsync(new TodoItemNote());
            var service = new DeleteNoteService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.DeleteNote(Guid.NewGuid());

            Assert.IsInstanceOf<NoteDeletedResult>(result);
        }
    }
}
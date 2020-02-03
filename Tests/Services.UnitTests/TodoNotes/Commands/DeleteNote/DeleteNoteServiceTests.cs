using System;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoNotes.Commands.DeleteNote;

namespace Todo.Services.UnitTests.TodoNotes.Commands.DeleteNote
{
    [TestFixture]
    public class DeleteNoteServiceTests
    {
        [Test]
        public void DeleteNote_WhenNoteDoesNotExist_ThrowsNotFoundException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new DeleteNoteService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteNote(Guid.NewGuid()));
        }
    }
}
using System;
using Data.Repositories;
using MediatR;
using Moq;
using NUnit.Framework;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
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
            var mockMediator = new Mock<IMediator>();

            var service = new DeleteNoteService(mockRepository.Object, mockMediator.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteNote(Guid.NewGuid()));
        }
    }
}
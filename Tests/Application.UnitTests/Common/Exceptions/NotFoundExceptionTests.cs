using System;
using AutoFixture;
using Todo.Common.Exceptions;
using NUnit.Framework;

namespace Todo.Common.UnitTests.Exceptions
{
    [TestFixture]
    public class NotFoundExceptionTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void WhenThrown_WithKeyAsInt_ReturnsCorrectMessage()
        {
            // Arrange
            var key = _fixture.Create<Guid>();
            var entityName = _fixture.Create<string>();

            // Act
            var exception = Assert.Catch<NotFoundException>(() => throw new NotFoundException(entityName, key));

            // Assert
            Assert.AreEqual($"{entityName} ({key}) record was not found.", exception.Message);
        }

        [Test]
        public void WhenThrown_WithSpecifiedKey_ReturnsCorrectMessage()
        {
            // Arrange
            var key = _fixture.Create<int>();
            var entityName = _fixture.Create<string>();

            // Act
            var exception = Assert.Catch<NotFoundException<int>>(() => throw new NotFoundException<int>(entityName, key));

            // Assert
            Assert.AreEqual($"{entityName} ({key}) record was not found.", exception.Message);
        }
    }
}
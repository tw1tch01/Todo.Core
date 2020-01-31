using AutoFixture;
using NUnit.Framework;
using Todo.Services.Common.Exceptions;

namespace Todo.Services.UnitTests.Common.Exceptions
{
    [TestFixture]
    public class BadRequestExceptionTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void WhenThrown_ReturnsCorrectMessage()
        {
            var message = _fixture.Create<string>();
            var exception = Assert.Catch<BadRequestException>(() => throw new BadRequestException(message));
            Assert.AreEqual(message, exception.Message);
        }
    }
}
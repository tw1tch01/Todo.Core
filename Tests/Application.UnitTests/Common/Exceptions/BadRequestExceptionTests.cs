using Todo.Common.Exceptions;
using NUnit.Framework;
using AutoFixture;

namespace Todo.Common.UnitTests.Exceptions
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
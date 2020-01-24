using System;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Todo.Application.Common.Specifications;
using Todo.Domain.Common;

namespace Todo.Application.UnitTests.Common.Specifications
{
    [TestFixture]
    public class ModifiedProcessTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedBy_WhenValueIsNullOrWhitespace_ThrowsArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() => new ModifiedProcess<IModifiedAudit>(value));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedProcess_WhenModifiedProcessIsNull_ReturnsFalse(string value)
        {
            var modifiedProcess = _fixture.Create<string>();
            var mockEntity = new Mock<IModifiedAudit>();
            mockEntity.Setup(a => a.ModifiedProcess).Returns(value);
            var specification = new ModifiedProcess<IModifiedAudit>(modifiedProcess);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedProcess_WhenModifiedProcessDoesNotMatchValue_ReturnsFalse()
        {
            var modifiedProcess = _fixture.Create<string>();
            var mockEntity = new Mock<IModifiedAudit>();
            mockEntity.Setup(a => a.ModifiedProcess).Returns(_fixture.Create<string>());
            var specification = new ModifiedProcess<IModifiedAudit>(modifiedProcess);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedProcess_WhenModifiedProcessMatchesValue_ReturnsTrue()
        {
            var modifiedProcess = _fixture.Create<string>();
            var mockEntity = new Mock<IModifiedAudit>();
            mockEntity.Setup(a => a.ModifiedProcess).Returns(modifiedProcess);
            var specification = new ModifiedProcess<IModifiedAudit>(modifiedProcess);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsTrue(satisified);
        }
    }
}
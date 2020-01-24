using System;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Todo.Application.Common.Specifications;
using Todo.Domain.Common;

namespace Todo.Application.UnitTests.Common.Specifications
{
    [TestFixture]
    public class CreatedByTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedBy_WhenValueIsNullOrWhitespace_ThrowsArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() => new CreatedBy<ICreatedAudit>(value));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedBy_WhenCreatedByIsNull_ReturnsFalse(string value)
        {
            var createdBy = _fixture.Create<string>();
            var mockEntity = new Mock<ICreatedAudit>();
            mockEntity.Setup(a => a.CreatedBy).Returns(value);
            var specification = new CreatedBy<ICreatedAudit>(createdBy);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenCreatedByDoesNotMatchValue_ReturnsFalse()
        {
            var createdBy = _fixture.Create<string>();
            var mockEntity = new Mock<ICreatedAudit>();
            mockEntity.Setup(a => a.CreatedBy).Returns(_fixture.Create<string>());
            var specification = new CreatedBy<ICreatedAudit>(createdBy);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenCreatedByMatchesValue_ReturnsTrue()
        {
            var createdBy = _fixture.Create<string>();
            var mockEntity = new Mock<ICreatedAudit>();
            mockEntity.Setup(a => a.CreatedBy).Returns(createdBy);
            var specification = new CreatedBy<ICreatedAudit>(createdBy);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsTrue(satisified);
        }
    }
}
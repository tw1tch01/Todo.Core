﻿using System;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Todo.Domain.Common;
using Todo.Services.Common.Specifications;

namespace Todo.Services.UnitTests.Common.Specifications
{
    [TestFixture]
    public class CreatedProcessTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedProcess_WhenValueIsNullOrWhitespace_ThrowsArgumentException(string value)
        {
            Assert.Throws<ArgumentException>(() => new CreatedProcess<ICreatedAudit>(value));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedProcess_WhenCreatedProcessIsEmpty_ReturnsFalse(string value)
        {
            var createdProcess = _fixture.Create<string>();
            var mockEntity = new Mock<ICreatedAudit>();
            mockEntity.Setup(a => a.CreatedProcess).Returns(value);
            var specification = new CreatedProcess<ICreatedAudit>(createdProcess);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedProcess_WhenCreatedProcessDoesNotMatchValue_ReturnsFalse()
        {
            var createdProcess = _fixture.Create<string>();
            var mockEntity = new Mock<ICreatedAudit>();
            mockEntity.Setup(a => a.CreatedProcess).Returns(_fixture.Create<string>());
            var specification = new CreatedProcess<ICreatedAudit>(createdProcess);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedProcess_WhenCreatedProcessMatchesValue_ReturnsTrue()
        {
            var createdProcess = _fixture.Create<string>();
            var mockEntity = new Mock<ICreatedAudit>();
            mockEntity.Setup(a => a.CreatedProcess).Returns(createdProcess);
            var specification = new CreatedProcess<ICreatedAudit>(createdProcess);
            var satisified = specification.IsSatisfiedBy(mockEntity.Object);
            Assert.IsTrue(satisified);
        }
    }
}
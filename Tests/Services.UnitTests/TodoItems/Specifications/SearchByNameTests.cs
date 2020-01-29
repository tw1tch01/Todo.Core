using System;
using AutoFixture;
using NUnit.Framework;
using Todo.Application.TodoItems.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class SearchByNameTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedBy_WhenValueIsNullOrWhiteSpace_ThrowsArgumentNullException(string value)
        {
            Assert.Throws<ArgumentException>(() => new SearchByName(value));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsSatisfiedBy_WhenNameIsNullOrWhiteSpace_ReturnsFalse(string name)
        {
            var value = _fixture.Create<string>();
            var item = new TodoItem
            {
                Name = name
            };
            var searchByName = new SearchByName(value);
            var satisfied = searchByName.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenNameDoesNotMatchValue_ReturnsFalse()
        {
            var value = _fixture.Create<string>();
            var item = new TodoItem
            {
                Name = _fixture.Create<string>()
            };
            var searchByName = new SearchByName(value);
            var satisfied = searchByName.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenNameMatchesValue_ReturnsTrue()
        {
            var value = _fixture.Create<string>();
            var item = new TodoItem
            {
                Name = value
            };
            var searchByName = new SearchByName(value);
            var satisfied = searchByName.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenNameDoesNotContainValue_ReturnsFalse()
        {
            var value = _fixture.Create<string>();
            var item = new TodoItem
            {
                Name = value.Substring(5, 5)
            };
            var searchByName = new SearchByName(value);
            var satisfied = searchByName.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenNameContainsValue_ReturnsTrue()
        {
            var value = _fixture.Create<string>();
            var item = new TodoItem
            {
                Name = value
            };
            var searchByName = new SearchByName(value.Substring(5, 5));
            var satisfied = searchByName.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }
    }
}
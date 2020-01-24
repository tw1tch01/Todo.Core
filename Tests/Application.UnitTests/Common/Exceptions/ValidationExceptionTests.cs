using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Todo.Common.Exceptions;
using FluentValidation.Results;
using NUnit.Framework;

namespace Todo.Common.UnitTests.Exceptions
{
    [TestFixture]
    public class ValidationExceptionTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void WhenThrown_WithoutFailures_ReturnsCorrectMessage()
        {
            var message = ValidationException._errorMessage;
            var exception = Assert.Catch<ValidationException>(() => throw new ValidationException());

            Assert.Multiple(() =>
            {
                Assert.AreEqual(message, exception.Message);
                Assert.IsNotNull(exception.Failures);
            });
        }

        [Test]
        public void WhenThrown_WithFailures_ReturnsCorrectMessageWithFailures()
        {
            var message = ValidationException._errorMessage;
            var failure = new ValidationFailure(_fixture.Create<string>(), _fixture.Create<string>());
            var validationFailures = new List<ValidationFailure> { failure };
            var exception = Assert.Catch<ValidationException>(() => throw new ValidationException(validationFailures));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(message, exception.Message);
                Assert.IsNotNull(exception.Failures);
                Assert.AreEqual(1, exception.Failures.Count);
                Assert.AreEqual(1, exception.Failures.Keys.Count);
                Assert.AreEqual(1, exception.Failures.Values.SelectMany(v => v).Count());
            });
        }

        [Test]
        public void GetValidationFailures_WhenCollectionIsNull_ReturnsEmptyDictionary()
        {
            var results = ValidationException.GetValidationFailures(null);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(results);
                Assert.IsEmpty(results);
            });
        }

        [Test]
        public void GetValidationFailures_WhenCollectionIsEmpty_ReturnsEmptyDictionary()
        {
            var results = ValidationException.GetValidationFailures(new List<ValidationFailure>());
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(results);
                Assert.IsEmpty(results);
            });
        }

        [Test]
        public void GetValidationFailures_WhenSinglePropertyFailsWithSingleMessage_ReturnsDictionaryWithSingleKeySingleValue()
        {
            var failure = new ValidationFailure(_fixture.Create<string>(), _fixture.Create<string>());
            var validationFailures = new List<ValidationFailure> { failure };
            var results = ValidationException.GetValidationFailures(validationFailures);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(results);
                Assert.AreEqual(1, results.Count);
                Assert.AreEqual(1, results.Keys.Count);
                Assert.AreEqual(1, results.Values.SelectMany(v => v).Count());
            });
        }

        [Test]
        public void GetValidationFailures_WhenSinglePropertyFailsWithMultipleMessages_ReturnsDictionaryWithSingleKeyMultipleValues()
        {
            var propertyName = _fixture.Create<string>();
            var failure1 = new ValidationFailure(propertyName, _fixture.Create<string>());
            var failure2 = new ValidationFailure(propertyName, _fixture.Create<string>());
            var validationFailures = new List<ValidationFailure> { failure1, failure2 };
            var results = ValidationException.GetValidationFailures(validationFailures);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(results);
                Assert.AreEqual(1, results.Count);
                Assert.AreEqual(1, results.Keys.Count);
                Assert.AreEqual(2, results.Values.SelectMany(v => v).Count());
            });
        }

        [Test]
        public void GetValidationFailures_WhenMultiplePropertiesFailWithMultipleMessages_ReturnsDictionaryWithMultipleKeysAndMultipleValues()
        {
            var propertyName = _fixture.Create<string>();
            var failure1 = new ValidationFailure(propertyName, _fixture.Create<string>());
            var failure2 = new ValidationFailure(propertyName, _fixture.Create<string>());
            var failure3 = new ValidationFailure(_fixture.Create<string>(), _fixture.Create<string>());
            var validationFailures = new List<ValidationFailure> { failure1, failure2, failure3 };
            var results = ValidationException.GetValidationFailures(validationFailures);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(results);
                Assert.AreEqual(2, results.Count);
                Assert.AreEqual(2, results.Keys.Count);
                Assert.AreEqual(3, results.Values.SelectMany(v => v).Count());
            });
        }

        [Test]
        public void GetValidationFailures_WhenSameErrorIsAddedTwice_OnlyReturnsFailureOnce()
        {
            var failure = new ValidationFailure(_fixture.Create<string>(), _fixture.Create<string>());
            var validationFailures = new List<ValidationFailure> { failure, failure };
            var results = ValidationException.GetValidationFailures(validationFailures);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(results);
                Assert.AreEqual(1, results.Count);
                Assert.AreEqual(1, results.Keys.Count);
                Assert.AreEqual(1, results.Values.SelectMany(v => v).Count());
            });
        }
    }
}
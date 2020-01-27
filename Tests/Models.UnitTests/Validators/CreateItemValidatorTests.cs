using System.Threading.Tasks;
using AutoFixture;
using NUnit.Framework;
using Todo.Models.TodoItems;
using Todo.Models.Validators;

namespace Todo.Models.UnitTests.Validators
{
    [TestFixture]
    public class CreateItemValidatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task WhenNameIsEmpty_ReturnsValidationErrorForName(string name)
        {
            var dto = new CreateItemDto
            {
                Name = name,
                Description = _fixture.Create<string>()
            };

            var validator = new CreateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"'{nameof(CreateItemDto.Name)}' must not be empty.", results.Errors[0].ErrorMessage);
            });
        }

        [Test]
        public async Task WhenNameLengthIsGreaterThan64_ReturnsValidationErrorForName()
        {
            var dto = new CreateItemDto
            {
                Name = GenerateString(64),
                Description = _fixture.Create<string>()
            };

            var validator = new CreateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"The length of '{nameof(CreateItemDto.Name)}' must be 64 characters or fewer. You entered {dto.Name.Length} characters.", results.Errors[0].ErrorMessage);
            });
        }

        [Test]
        public async Task WhenItemIsValid_ReturnsTrue()
        {
            var dto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };

            var validator = new CreateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(results.IsValid);
                Assert.AreEqual(0, results.Errors.Count);
            });
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task WhenDescriptionIsEmpty_ReturnsValidationErrorForDescription(string description)
        {
            var dto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = description
            };

            var validator = new CreateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"'{nameof(CreateItemDto.Description)}' must not be empty.", results.Errors[0].ErrorMessage);
            });
        }

        [Test]
        public async Task WhenDescriptionLengthIsGreaterThan1024_ReturnsValidationErrorForDescription()
        {
            var dto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = GenerateString(1024)
            };

            var validator = new CreateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"The length of '{nameof(CreateItemDto.Description)}' must be 1024 characters or fewer. You entered {dto.Description.Length} characters.", results.Errors[0].ErrorMessage);
            });
        }

        [Test]
        public async Task WhenRankIsLessThanZero_ReturnsValidationErrorForRank()
        {
            var dto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                Rank = -1
            };

            var validator = new CreateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"'{nameof(CreateItemDto.Rank)}' must be greater than or equal to '0'.", results.Errors[0].ErrorMessage);
            });
        }

        private string GenerateString(int v)
        {
            var @string = _fixture.Create<string>();
            while (@string.Length < v) @string += _fixture.Create<string>();
            return @string;
        }
    }
}
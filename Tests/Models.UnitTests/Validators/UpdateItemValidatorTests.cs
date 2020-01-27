using System.Threading.Tasks;
using AutoFixture;
using NUnit.Framework;
using Todo.Models.TodoItems;
using Todo.Models.Validators;

namespace Todo.Models.UnitTests.Validators
{
    [TestFixture]
    public class UpdateItemValidatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public async Task WhenNameLengthIsGreaterThan64_ReturnsValidationErrorForName()
        {
            var dto = new UpdateItemDto
            {
                Name = GenerateString(64)
            };

            var validator = new UpdateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"The length of '{nameof(UpdateItemDto.Name)}' must be 64 characters or fewer. You entered {dto.Name.Length} characters.", results.Errors[0].ErrorMessage);
            });
        }

        [Test]
        public async Task WhenItemHasNoChanges_ReturnsTrue()
        {
            var dto = new UpdateItemDto();

            var validator = new UpdateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(results.IsValid);
                Assert.AreEqual(0, results.Errors.Count);
            });
        }

        [Test]
        public async Task WhenItemIsValid_ReturnsTrue()
        {
            var dto = new UpdateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                Rank = 2
            };

            var validator = new UpdateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(results.IsValid);
                Assert.AreEqual(0, results.Errors.Count);
            });
        }

        [Test]
        public async Task WhenDescriptionLengthIsGreaterThan1024_ReturnsValidationErrorForDescription()
        {
            var dto = new UpdateItemDto
            {
                Description = GenerateString(1024)
            };

            var validator = new UpdateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"The length of '{nameof(UpdateItemDto.Description)}' must be 1024 characters or fewer. You entered {dto.Description.Length} characters.", results.Errors[0].ErrorMessage);
            });
        }

        [Test]
        public async Task WhenRankIsLessThanZero_ReturnsValidationErrorForRank()
        {
            var dto = new UpdateItemDto
            {
                Rank = -1
            };

            var validator = new UpdateItemValidator();
            var results = await validator.ValidateAsync(dto);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(results.IsValid);
                Assert.AreEqual(1, results.Errors.Count);
                Assert.AreEqual($"'{nameof(UpdateItemDto.Rank)}' must be greater than or equal to '0'.", results.Errors[0].ErrorMessage);
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
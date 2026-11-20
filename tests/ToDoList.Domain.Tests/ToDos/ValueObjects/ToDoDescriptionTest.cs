using Bogus;
using FluentAssertions;
using ToDoList.Domain.ToDos.Exceptions;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.Tests.ToDos.ValueObjects
{
    public class ToDoDescriptionTest
    {
        private readonly Faker faker = new();

        [Fact]
        public void SetDescription_ShouldKeepGivenValue()
        {
            string value = faker.Lorem.Paragraph();

            ToDoDescription description = ToDoDescription.SetDescription(value);

            description.Value.Should().Be(value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenDescriptionIsNullOrEmpty_ShouldHaveNullValue(string? value)
        {
            ToDoDescription description = ToDoDescription.SetDescription(value);

            description.Value.Should().BeNull();
        }

        [Fact]
        public void WhenDescriptionHasMaxLength_ShouldBeCreated()
        {
            string value = new string('a', ToDoDescription.MaxLength);

            ToDoDescription description = ToDoDescription.SetDescription(value);

            description.Value.Should().Be(value);
        }

        [Fact]
        public void WhenDescriptionExceedsMaxLength_ShouldThrowInvalidToDoException()
        {
            string value = new string('a', ToDoDescription.MaxLength + 1);

            Func<ToDoDescription> act = () => ToDoDescription.SetDescription(value);

            act.Should().Throw<InvalidToDoException>();
        }
    }
}

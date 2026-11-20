using Bogus;
using FluentAssertions;
using ToDoList.Domain.ToDos.Exceptions;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.Tests.ToDos.ValueObjects
{
    public class ToDoTitleTest
    {
        private readonly Faker faker = new();

        [Fact]
        public void SetTitle_ShouldKeepGivenValue()
        {
            string value = faker.Lorem.Sentence(3).TrimEnd('.');

            ToDoTitle title = ToDoTitle.SetTitle(value);

            title.Value.Should().Be(value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void WhenTitleIsNullOrWhiteSpace_ShouldThrowInvalidToDoException(string? value)
        {
            Func<ToDoTitle> act = () => ToDoTitle.SetTitle(value!);

            act.Should().Throw<InvalidToDoException>();
        }

        [Fact]
        public void WhenTitleHasMaxLength_ShouldBeCreated()
        {
            string value = new string('a', ToDoTitle.MaxLength);

            ToDoTitle title = ToDoTitle.SetTitle(value);

            title.Value.Should().Be(value);
        }

        [Fact]
        public void WhenTitleExceedsMaxLength_ShouldThrowInvalidToDoException()
        {
            string value = new string('a', ToDoTitle.MaxLength + 1);

            Func<ToDoTitle> act = () => ToDoTitle.SetTitle(value);

            act.Should().Throw<InvalidToDoException>();
        }
    }
}

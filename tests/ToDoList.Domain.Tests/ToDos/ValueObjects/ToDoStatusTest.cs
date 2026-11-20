using FluentAssertions;
using ToDoList.Domain.ToDos.Exceptions;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.Tests.ToDos.ValueObjects
{
    public class ToDoStatusTest
    {
        [Theory]
        [InlineData(ToDoStatusEnum.NotStarted)]
        [InlineData(ToDoStatusEnum.InProgress)]
        [InlineData(ToDoStatusEnum.Finished)]
        public void SetStatus_ShouldKeepGivenValue(ToDoStatusEnum value)
        {
            ToDoStatus status = ToDoStatus.SetStatus(value);

            status.Value.Should().Be(value);
        }

        [Fact]
        public void WhenStatusIsNotDefined_ShouldThrowInvalidToDoException()
        {
            ToDoStatusEnum value = (ToDoStatusEnum)999;

            Func<ToDoStatus> act = () => ToDoStatus.SetStatus(value);

            act.Should().Throw<InvalidToDoException>();
        }
    }
}

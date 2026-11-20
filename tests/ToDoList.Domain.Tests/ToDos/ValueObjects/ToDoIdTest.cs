using FluentAssertions;
using ToDoList.Domain.ToDos.Exceptions;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.Tests.ToDos.ValueObjects
{
    public class ToDoIdTest
    {
        [Fact]
        public void New_ShouldReturnNewGuid()
        {
            ToDoId id = ToDoId.New();

            id.Should().BeOfType<ToDoId>();
            ((object)id.Value).Should().BeOfType<Guid>();
            id.Value.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void From_ShouldReturnGuid()
        {
            Guid guid = Guid.NewGuid();
            ToDoId id = ToDoId.From(guid);

            id.Should().BeOfType<ToDoId>();
            ((object)id.Value).Should().BeOfType<Guid>();
            id.Value.Should().NotBe(Guid.Empty);
            id.Value.Should().Be(guid);
        }

        [Fact]
        public void GivenFromMethodExecuted_WhenParameterIsEmpty_ShouldThrowInvalidToDoException()
        {
            var act = () => ToDoId.From(Guid.Empty);

            act.Should().Throw<InvalidToDoException>();
        }
    }
}

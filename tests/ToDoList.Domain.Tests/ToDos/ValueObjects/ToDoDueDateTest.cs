using Bogus;
using FluentAssertions;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.Tests.ToDos.ValueObjects
{
    public class ToDoDueDateTest
    {
        private readonly Faker faker = new();

        [Fact]
        public void SetDate_ShouldKeepGivenDate()
        {
            DateTime value = faker.Date.Future();

            ToDoDueDate dueDate = ToDoDueDate.SetDate(value);

            dueDate.value.Should().Be(value);
        }

        [Fact]
        public void WhenDateIsNull_ShouldHaveNullValue()
        {
            ToDoDueDate dueDate = ToDoDueDate.SetDate(null);

            dueDate.value.Should().BeNull();
        }
    }
}

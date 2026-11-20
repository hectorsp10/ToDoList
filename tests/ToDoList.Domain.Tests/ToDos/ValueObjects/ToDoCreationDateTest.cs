using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using ToDoList.Domain.ToDos.Exceptions;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.Tests.ToDos.ValueObjects
{
    public class ToDoCreationDateTest
    {
        private readonly Faker faker = new();

        [Fact]
        public void SetDateNow_ShouldReturnCurrentUtcDate()
        {
            ToDoCreationDate creationDate = ToDoCreationDate.SetDateNow();

            using (new AssertionScope())
            {
                creationDate.Value.Kind.Should().Be(DateTimeKind.Utc);
                creationDate.Value.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            }
        }
    }
}

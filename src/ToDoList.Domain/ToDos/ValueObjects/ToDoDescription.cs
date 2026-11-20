
using ToDoList.Domain.ToDos.Exceptions;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public class ToDoDescription
    {
        public string? Value { get; }

        public const int MaxLength = 1000;

        private ToDoDescription(string? value)
        {
            Value = value;
        }

        public static ToDoDescription SetDescription(string? value)
        {
            if (value != null && value.Length > MaxLength)
            {
                throw new InvalidToDoException($"ToDoDescription exceeds maximum length of {MaxLength} characters");
            }

            if (value == string.Empty)
            {
                value = null;
            }

            return new ToDoDescription(value);
        }
    }
}

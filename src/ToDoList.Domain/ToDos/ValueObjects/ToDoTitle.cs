
using ToDoList.Domain.ToDos.Exceptions;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public class ToDoTitle
    {
        public string Value { get; }

        public const int MaxLength = 200;

        private ToDoTitle(string value)
        {
            Value = value;
        }

        public static ToDoTitle SetTitle(string value)
        {
            if (value == null)
            {
                throw new InvalidToDoException("ToDoTitle cannot be null");
            }
            if (value.Trim() == string.Empty)
            {
                throw new InvalidToDoException("ToDoTitle cannot be empty");
            }
            if (value.Length > MaxLength)
            {
                throw new InvalidToDoException($"ToDoDescription exceeds maximum length of {MaxLength} characters");
            }

            return new ToDoTitle(value);
        }
    }
}

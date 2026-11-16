
using ToDoList.Domain.ToDos.Exceptions;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public class ToDoDescription
    {
        public string? Value { get; }

        private ToDoDescription(string? value)
        {
            Value = value;
        }

        public static ToDoDescription SetDescription(string? value)
        {
            if (value == string.Empty)
            {
                throw new InvalidToDoException("ToDoDescription cannot be empty");
            }

            return new ToDoDescription(value);
        }
    }
}

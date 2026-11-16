
using ToDoList.Domain.ToDos.Exceptions;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public class ToDoTitle
    {
        public string Value { get; }

        private ToDoTitle(string value)
        {
            Value = value;
        }

        public static ToDoTitle SetTitle(string value)
        {
            if (value == string.Empty)
            {
                throw new InvalidToDoException("ToDoTitle cannot be empty");
            }
            if (value == null)
            {
                throw new InvalidToDoException("ToDoTitle cannot be null");
            }

            return new ToDoTitle(value);
        }
    }
}

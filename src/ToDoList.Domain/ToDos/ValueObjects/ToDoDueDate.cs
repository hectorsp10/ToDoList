
using ToDoList.Domain.ToDos.Exceptions;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public sealed class ToDoDueDate
    {
        public DateTime value { get; }

        private ToDoDueDate(DateTime value)
        {
            this.value = value;
        }

        public static ToDoDueDate SetDate(DateTime value)
        {
            return new ToDoDueDate(value);
        }
    }
}

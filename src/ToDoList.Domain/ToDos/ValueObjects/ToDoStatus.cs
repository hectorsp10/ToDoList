using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public sealed class ToDoStatus
    {
        public ToDoStatusEnum Value { get; }

        private ToDoStatus(ToDoStatusEnum value)
        {
            Value = value;
        }

        public static ToDoStatus SetStatus(ToDoStatusEnum value)
        {
            return new ToDoStatus(value);
        }
    }
}

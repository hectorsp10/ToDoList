using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domain.ToDos.Exceptions;

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
            if(!Enum.IsDefined(typeof(ToDoStatusEnum), value))
            {
                throw new InvalidToDoException($"Status value ({value}) is not defined in ToDoStatusEnum");
            }            

            return new ToDoStatus(value);
        }
    }
}

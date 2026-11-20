using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public class ToDoCreationDate
    {
        public DateTime Value { get; }

        private ToDoCreationDate(DateTime value)
        {
            this.Value = value;
        }

        public static ToDoCreationDate SetDateNow()
        {
            return new ToDoCreationDate(DateTime.UtcNow);
        }
    }
}

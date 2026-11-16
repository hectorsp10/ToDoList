using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.ToDos.Exceptions
{
    public sealed class InvalidToDoException : Exception
    {
        public InvalidToDoException(string message) : base(message)
        {
        }
    }
}

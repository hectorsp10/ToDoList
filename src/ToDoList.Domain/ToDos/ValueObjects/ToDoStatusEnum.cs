using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public enum ToDoStatusEnum
    {
        NotStarted = 0,
        InProgress = 1,
        Finished = 2,
        Cancelled = 3
    }
}

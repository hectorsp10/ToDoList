using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Application.ToDos.Services.Create
{
    public sealed record ToDoCreateCommand
    {
        public string Title { get; }
        public string? Description { get; }
        public DateTime? DueDate { get; }

        public ToDoCreateCommand(string title, string? description, DateTime? dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }
    }
}

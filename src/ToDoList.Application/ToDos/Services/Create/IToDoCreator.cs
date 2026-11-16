using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Application.ToDos.DTOs;

namespace ToDoList.Application.ToDos.Services.Create
{
    public interface IToDoCreator
    {
        Task<ToDoDto> CreateAsync(ToDoCreateCommand toDoCreateCommand, CancellationToken cancellationToken);
    }
}

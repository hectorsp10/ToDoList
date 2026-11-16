using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domain.ToDos.Entities;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.ToDos.Repositories
{
    public interface IToDoRepository
    {
        Task<ToDoEntity?> GetByIdAsync(ToDoId toDoId, CancellationToken cancellationToken);
        Task<List<ToDoEntity>> ListAllAsync(CancellationToken cancellationToken);
        Task CreateAsync(ToDoEntity toDoEntity, CancellationToken cancellationToken);
        Task UpdateAsync(ToDoEntity toDoEntity, CancellationToken cancellationToken);
        Task DeleteAsync(ToDoId toDoId, CancellationToken cancellationToken);
    }
}

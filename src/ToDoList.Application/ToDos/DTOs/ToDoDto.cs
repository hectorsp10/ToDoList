using ToDoList.Domain.ToDos.Entities;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Application.ToDos.DTOs;

public sealed record ToDoDto(
    Guid Id,
    string Title,
    string? Description,
    ToDoStatusEnum Status,
    DateTime CreationDate,
    DateTime? DueDate)
{
    public static ToDoDto FromEntity(ToDoEntity entity) => new(
        entity.ToDoId.Value,
        entity.ToDoTitle.Value,
        entity.ToDoDescription.Value,
        entity.ToDoStatus.Value,
        entity.ToDoCreationDate.Value,
        entity.ToDoDueDate?.value);
}

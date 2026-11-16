using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Application.ToDos.DTOs;
using ToDoList.Domain.ToDos.Entities;
using ToDoList.Domain.ToDos.Repositories;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Application.ToDos.Services.Create
{
    internal class ToDoCreator : IToDoCreator
    {
        private readonly IToDoRepository toDoRepository;

        public ToDoCreator(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }

        public async Task<ToDoDto> CreateAsync(ToDoCreateCommand toDoCreateCommand, CancellationToken cancellationToken)
        {
            ToDoEntity toDoEntity = ToDoEntity.Create(
                toDoCreateCommand.Title,
                toDoCreateCommand.Description,
                toDoCreateCommand.DueDate
            );

            await toDoRepository.CreateAsync(toDoEntity, cancellationToken);

            return ToDoDto.FromEntity(toDoEntity);
        }
    }
}

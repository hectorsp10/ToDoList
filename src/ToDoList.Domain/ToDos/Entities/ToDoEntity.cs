using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.ToDos.Entities
{
    public class ToDoEntity
    {
        public ToDoId ToDoId { get; private set; }
        public ToDoTitle ToDoTitle { get; private set; }
        public ToDoDescription ToDoDescription { get; private set; }
        public ToDoStatus ToDoStatus { get; private set; }
        public ToDoCreationDate ToDoCreationDate { get; private set; }
        public ToDoDueDate ToDoDueDate { get; private set; }

        public ToDoEntity(
            ToDoId toDoId, 
            ToDoTitle toDoTitle, 
            ToDoDescription toDoDescription, 
            ToDoStatus toDoStatus, 
            ToDoCreationDate toDoCreationDate, 
            ToDoDueDate toDoDueDate)
        {
            ToDoId = toDoId;
            ToDoTitle = toDoTitle;
            ToDoDescription = toDoDescription;
            ToDoStatus = toDoStatus;
            ToDoCreationDate = toDoCreationDate;
            ToDoDueDate = toDoDueDate;
        }

        public static ToDoEntity Create(
            string toDoTitle,
            string? toDoDescription,
            DateTime? toDoDueDate)
        {
            return new ToDoEntity(
                ToDoId.New(),
                ToDoTitle.SetTitle(toDoTitle),
                ToDoDescription.SetDescription(toDoDescription),
                ToDoStatus.SetStatus(ToDoStatusEnum.NotStarted),
                ToDoCreationDate.SetDateNow(),
                ToDoDueDate.SetDate(toDoDueDate)
            );
        }

        public void UpdateTitle(string toDoTitle)
        {
            ToDoTitle = ToDoTitle.SetTitle(toDoTitle);
        }

        public void UpdateDescription(string toDoDescription)
        {
            ToDoDescription = ToDoDescription.SetDescription(toDoDescription);
        }

        public void UpdateStatus(ToDoStatusEnum toDoStatus)
        {
            ToDoStatus = ToDoStatus.SetStatus(toDoStatus);
        }

        public void UpdateDueDate(DateTime toDoDueDate)
        {
            ToDoDueDate = ToDoDueDate.SetDate(toDoDueDate);
        }
    }
}

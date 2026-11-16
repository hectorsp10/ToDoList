using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domain.ToDos.ValueObjects;

namespace ToDoList.Domain.ToDos.Entities
{
    public class ToDoEntity
    {
        public ToDoId ToDoId { get; set; }
        public ToDoTitle ToDoTitle { get; set; }
        public ToDoDescription ToDoDescription { get; set; }
        public ToDoStatus ToDoStatus { get; set; }
        public ToDoCreationDate ToDoCreationDate { get; set; }
        public ToDoDueDate ToDoDueDate { get; set; }

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

        public ToDoEntity Create(
            ToDoId toDoId,
            ToDoTitle toDoTitle,
            ToDoDescription toDoDescription,
            ToDoStatus toDoStatus,
            ToDoCreationDate toDoCreationDate,
            ToDoDueDate toDoDueDate)
        {
            return new ToDoEntity(
                toDoId,
                toDoTitle,
                toDoDescription,
                toDoStatus,
                toDoCreationDate,
                toDoDueDate);
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

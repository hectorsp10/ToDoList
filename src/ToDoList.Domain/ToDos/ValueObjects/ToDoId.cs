using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domain.ToDos.Exceptions;

namespace ToDoList.Domain.ToDos.ValueObjects
{
    public sealed record ToDoId 
        // Se utiliza sealed record porque un value object
        // se define por su valor, no por identidad
    {
        public Guid Value { get; }

        // Constructor privado (solo se podrá crear un ToDoId a través de los métodos públicos
        private ToDoId(Guid value)
        {
            Value = value;
        }

        // Método para crear un nuevo ToDoId random
        public static ToDoId New()
        {
            return new ToDoId(Guid.NewGuid());
        }

        // Método para crear un ToDoId a partir de un Guid existente
        public static ToDoId From(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidToDoException("ToDoId cannot be empty.");
            }

            return new ToDoId(value);
        }
    }
}

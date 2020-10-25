using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMapServer.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; }
        //TODO: прописать в наследниках вызов родительского конструктора
        //TODO: ? заменить id на guid
        public Entity(int id)
        {
            Id = id;
        }
    }
}

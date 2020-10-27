using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMapServer.Models
{
    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        //TODO: прописать в наследниках вызов родительского конструктора
        //TODO: ? заменить id на guid
        //public Entity()
        //{
        //    Id = new Guid();
        //}
    }
}

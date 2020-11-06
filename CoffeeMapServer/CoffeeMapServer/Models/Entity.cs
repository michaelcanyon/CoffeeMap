using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMapServer.Models
{
    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; }

        protected Entity(Guid? id) 
            => Id = id is null 
                ? Guid.NewGuid()
                : id.Value;
    }
}
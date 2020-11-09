using System;

namespace CoffeeMapServer.Models
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid? id = null)
            => Id = id is null ? Guid.NewGuid() : id.Value;
    }
}
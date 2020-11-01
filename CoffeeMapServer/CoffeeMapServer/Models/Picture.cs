using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMapServer.Models
{
    public class Picture : Entity
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}

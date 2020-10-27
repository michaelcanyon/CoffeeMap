using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.Models
{
    public class Tag : Entity
    {
        [Required]
        public string TagTitle { get; set; }

        //public Tag() : base()
        //{ }
    }
}

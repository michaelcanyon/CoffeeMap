using System.ComponentModel.DataAnnotations;
namespace CoffeeMapServer.Models
{
    public class User : Entity
    {
        [Required(ErrorMessage = "Не указан логин")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string role { get; set; }

        //public User() : base()
        //{ }
    }
}

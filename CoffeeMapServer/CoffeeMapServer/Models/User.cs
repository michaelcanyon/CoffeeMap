using System;
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
        public string Role { get; set; }

        public User() { }

        private User(Guid? id = null) : base(id)
        { }

        public static User New(Guid id,
                               string login,
                               string email,
                               string password,
                               string role)
            => new User(id)
            {
                Login = login,
                Email = email,
                Password = password,
                Role = role
            };

        public static User New(string login,
                               string email,
                               string password,
                               string role)
            => new User
            {
                Login = login,
                Email = email,
                Password = password,
                Role = role
            };
    }
}
using System;
using System.ComponentModel.DataAnnotations;
namespace CoffeeMapServer.Models
{
    public class User : Entity
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

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
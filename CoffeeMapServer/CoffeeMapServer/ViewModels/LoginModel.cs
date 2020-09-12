using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

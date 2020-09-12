using System.ComponentModel.DataAnnotations;

namespace CoffeeMapServer.ViewModels
{
    public class RegisterModelcs
    {
        [Required(ErrorMessage ="Не указан email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}

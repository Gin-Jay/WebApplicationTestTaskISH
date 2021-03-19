using System.ComponentModel.DataAnnotations;

namespace WebApplicationTestTaskISH.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым.")]
        [MaxLength(50), MinLength(2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле 'Email' не может быть пустым.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Пожалуйста, введите корректный email (формат: example@example.com)")]
        [MaxLength(50), MinLength(2)]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public UserRoles? Role { get; set; }
    }
}

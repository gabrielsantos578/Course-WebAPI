using System.ComponentModel.DataAnnotations;

namespace CourseGuide.Objects.Contracts
{
    public class Login
    {
        [Required(ErrorMessage = "O e-mail é requerido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é requerida!")]
        public string Password { get; set; }
    }
}
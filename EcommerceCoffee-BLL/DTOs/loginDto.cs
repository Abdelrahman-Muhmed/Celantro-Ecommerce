using System.ComponentModel.DataAnnotations;

namespace EcommerceCoffee_BLL.DTOs
{
    public class loginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PassWord is required")]
        public string PassWord { get; set; }



    }
}

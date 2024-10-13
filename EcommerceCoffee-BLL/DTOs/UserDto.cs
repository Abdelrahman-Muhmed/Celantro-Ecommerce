using System.ComponentModel.DataAnnotations;

namespace commerceCoffee_BLL.DTOs
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public string password { get; set; }


        [Required]
        public string Token { get; set; }
    }
}

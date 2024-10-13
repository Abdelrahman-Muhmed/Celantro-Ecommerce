
using System;

namespace EcommerceCoffee_BLL.DTOs
{
    public class ApplicationUserReturnDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }

        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
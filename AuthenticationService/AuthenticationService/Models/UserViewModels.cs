using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
    }
    public class LoginSuccessViewModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public object Token { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
    }
    public class ListUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateUserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }

}

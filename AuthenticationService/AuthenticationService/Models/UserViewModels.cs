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
    public class UserModelDetail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
    public class UserProfileModels
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public string FullName { get; set; }

    }
    public class UserAdminModels
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public RoleModel Role { get; set; }
        public bool? IsActive { get; set; }
        public string Password { get; set; }
    }
    public class RoleModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class UserDetailModels
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public RoleModel Role { get; set; }
        public bool? IsActive { get; set; }
        public List<int> IdMarketer { get; set; }
        public List<int> IdEditor { get; set; }
        public List<int> IdWriter { get; set; }
        public List<ListUserModel> ChoiceMarketer { get; set; }
        public List<ListUserModel> ChoiceEditor { get; set; }
        public List<ListUserModel> ChoiceWriter { get; set; }
        public List<ListUserModel> Marketer { get; set; }
        public List<ListUserModel> Editor { get; set; }
        public List<ListUserModel> Writer { get; set; }
    }
    

}

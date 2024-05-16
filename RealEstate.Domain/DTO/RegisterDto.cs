using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.DTO
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "There must be Upper case letters and ower case letters and numbers and must be minimum 8 characters and cannot contain illegal characters, such as =, <, >, ;, :, ' in Password")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The Password not Matched")]
        public string ConfirmPassword { get; set; }
    }
}

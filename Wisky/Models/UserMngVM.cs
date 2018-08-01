using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DSS.Annotaion;

namespace DSS.Models
{
    public class UserMngVM
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Please input user name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please input password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please input email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please input full name.")]
        public string FullName { get; set; }
        [Required]
        public int BrandID { get; set; }
        public bool isActive { get; set; }
        public string BrandName { get; set; }
    }

    public class UserDetailVM
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Please input password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        //[UniqueEmail(ErrorMessage = "Email already exists. Please enter a different email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter full name.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s]+$", ErrorMessage = "Please enter full name only letter.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please selete Brand.")]
        public int BrandId { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required(ErrorMessage = "Please selete role.")]
        public string Role { get; set; }
    }

    public class CurrentUserVM
    {
        public String Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
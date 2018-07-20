using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DSS.Models
{
    public class BrandUserMngVM
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        //[UniqueEmail(ErrorMessage = "Email already exists. Please enter a different email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter full name.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter phone number.")]
        public int BrandID { get; set; }
        public bool isActive { get; set; }
        public string BrandName { get; set; }
    }

    public class BrandUserDetailVM
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }
        //[Required]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter full name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter full name only letter.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required(ErrorMessage = "Please selete role.")]
        public string Role { get; set; }
    }
}
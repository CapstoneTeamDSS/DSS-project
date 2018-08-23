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
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Fullname")]
        public string FullName { get; set; }
        [Required]
        public int BrandID { get; set; }
        public bool isActive { get; set; }
        public string BrandName { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserDetailVM
    {
        public String Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Username")]
        [UniqueUserName(ErrorMessage = "UserName already exists. Please enter a different username")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Please input password.")]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InEmail")]
        //[UniqueEmail(ErrorMessage = "Email already exists. Please enter a different email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Fullname")]
        [RegularExpression(@"^[\p{L}\p{M}\s'-]{1,40}$", ErrorMessage = "Please enter full name only letter.")]
        [StringLength(50, MinimumLength = 8, ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InName")]
        public string FullName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Brand")]
        public int BrandId { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InPhone")]
        public string PhoneNumber { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Role")]
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
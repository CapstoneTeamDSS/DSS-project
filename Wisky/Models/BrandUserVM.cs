using DSS.Annotaion;
using System;
using System.ComponentModel.DataAnnotations;

namespace DSS.Models
{

    public class BrandUserMngVM
    {
        public String Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Fullname")]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InEmail")]
        //[UniqueEmail(ErrorMessage = "Email already exists. Please enter a different email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Fullname")]
        [StringLength(50, MinimumLength = 8, ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InName")]
        public string FullName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Phone")]
        public int BrandID { get; set; }
        public bool IsActive { get; set; }
        public string BrandName { get; set; }
    }

    public class BrandAccountInformationVMs
    {
        public String Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Fullname")]
        [RegularExpression(@"^[\p{L}\p{M}\s'-]{1,40}$", ErrorMessage = "Please enter full name only letter.")]
        [StringLength(50, MinimumLength = 8, ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InName")]
        public string FullName { get; set; }
        //[Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InPhone")]
        public string PhoneNumber { get; set; }
    }
    public class BrandUserDetailVM
    {
        public String Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Username")]
        public string UserName { get; set; }
        //[Required]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InEmail")]
        [UniqueEmail(ErrorMessage = "Email already exists. Please enter a different email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Fullname")]
        [RegularExpression(@"^[\p{L}\p{M}\s'-]{1,40}$", ErrorMessage = "Please enter full name only letter.")]
        [StringLength(50, MinimumLength = 8,ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InName")]
        public string FullName { get; set; }
        //[Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "InPhone")]
        public string PhoneNumber { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Role")]
        public string Role { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


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
        [Required(ErrorMessage = "Please input user name.")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Please input password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please input email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please input full name.")]
        public string FullName { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please input phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required]
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
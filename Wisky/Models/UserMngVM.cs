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
        [Required(ErrorMessage = "Day la loi Name bi rong")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int BrandID { get; set; }
        public bool isActive { get; set; }
        public string BrandName { get; set; }
    }

    public class UserDetailVM
    {
        public String Id { get; set; }
        [Required(ErrorMessage = "Day la loi Name bi rong")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int BrandId { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
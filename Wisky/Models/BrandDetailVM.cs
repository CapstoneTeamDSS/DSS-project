using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class BrandDetailVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourcesLanguage.AccountError), ErrorMessageResourceName = "Brand")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
    }
}
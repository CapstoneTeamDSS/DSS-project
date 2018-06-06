using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Wisky.Models
{
    public class BrandViewModels
    {
    }

    public class AddBrandViewModel
    {
        [Required]
        [Display(Name = "Brandname")]
        public string BrandName { get; set; }

        [Display(Name = "Description")]
        public bool Discription { get; set; }
    }
}
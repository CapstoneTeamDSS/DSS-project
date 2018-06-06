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
        [Required (ErrorMessage ="Day la loi Name bi rong")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
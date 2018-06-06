using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class ResolutionDetailVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Day la loi Width bi rong")]
        public double Width { get; set; }
        [Required(ErrorMessage = "Day la loi Height bi rong")]
        public double Height { get; set; }
        public string Note { get; set; }
    }
}
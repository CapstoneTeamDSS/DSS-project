using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS.Models
{
    public class ResolutionDetailVM
    {
        public int? Id { get; set; }
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Please enter Height > 0!!!")]
        [Required(ErrorMessage = "Width can not be empty!!!")]
        public double Width { get; set; }
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Please enter Width > 0!!!")]
        [Required(ErrorMessage = "Height can not be empty!!!")]
        public double Height { get; set; }
        public string Note { get; set; }
    }
}
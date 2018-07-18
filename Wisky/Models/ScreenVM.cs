using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class ScreenVM
    {
        public int? ScreenId { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Please select location.")]
        public int LocationId { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Please select resolution.")]
        public int ResolutionId { get; set; }
        [Required(ErrorMessage = "Please input screen name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public bool isHorizontal { get; set; }

    }
}
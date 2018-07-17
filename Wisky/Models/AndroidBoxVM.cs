using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class AndroidBoxVM
    {
        public int? BoxId { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Please select Location")]
        public int LocationId { get; set; }
        [Required(ErrorMessage = "Please enter Box's name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
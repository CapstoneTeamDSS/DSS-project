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
        [Required(ErrorMessage = "Vui long dien Location")]
        public int LocationId { get; set; }
        [Required(ErrorMessage = "Vui long dien Resolution")]
        public int ResolutionId { get; set; }
        [Required(ErrorMessage = "Vui long dien ten Screen")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public bool isHorizontal { get; set; }

    }
}
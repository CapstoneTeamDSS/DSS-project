using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class ScenarioVM
    {
        public int? ScenarioId { get; set; }
        [Required(ErrorMessage = "Please select layout")]
        public int LayoutId { get; set; }
        [Required(ErrorMessage = "Please select title")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
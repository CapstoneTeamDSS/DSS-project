using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class MediaVM
    {
        public int? MediaSrcId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please enter Title")]
        public string Title { get; set; }
        public Boolean Status { get; set; }
        public int TypeId { get; set; }   
        public string URL { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDatetime { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}
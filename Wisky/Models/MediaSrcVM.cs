using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class MediaSrcVM
    {
        public int? MediaSrcId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please input title.")]
        public string Title { get; set; }
        public int TypeId { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDatetime { get; set; }
        public DateTime CreateDatetime { get; set; }
        public bool isActive { get; set; }
        public string Filename { get; set; }
    }

    public class MediaSrcUseVM
    {
        public int MediaSrcId { get; set; }
        public int BrandId { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS.Models
{
    public class LayoutDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsHorizontal { get; set; }
        public string Layoutsrc { get; set; }
        public bool ?IsPublic { get; set; }


    }
}
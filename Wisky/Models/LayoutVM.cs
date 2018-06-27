using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class LayoutVM
    {
        public int LayoutID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public bool? Type { get; set; }
        public string Description { get; set; }
    }
}
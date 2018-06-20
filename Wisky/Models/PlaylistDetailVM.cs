using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class PlaylistDetailVM
    {
        public int? Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }
        public string BrandName { get; set; }
    }
}
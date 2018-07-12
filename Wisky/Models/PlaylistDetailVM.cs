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
        public string Duration { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public bool isShow { get; set; }
    }

    public class PlaylistCRUDVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set   ; }
        public string Description { get; set; }
        public AddedElement[] AddedElements { get; set; }
    }

    public class AddedElement
    {
        public int ItemId { get; set; }
        public int ItemDuration { get; set; }
    }

}
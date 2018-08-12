using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class PlaylistItemVM
    {
        public int playlistItemId { get; set; }

        public int playlistId { get; set; }

        public int mediaSrcId { get; set; }

        public int displayOrder { get; set; }

        public int duration { get; set; }

        public string mediaSrcTitle { get; set; }

        public string URL { get; set; }

        public int mediaType { get; set; }
    }
}
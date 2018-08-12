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
        public bool IsPublic { get; set; }
    }

    public class ScenarioDetailVM
    {
        public int? ScenarioId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int LayoutId { get; set; }
        public PlaylistArea[] PlaylistAreaArr { get; set; }
        public bool IsPublic { get; set; }
        public int AudioArea { get; set; }
    }

    public class PlaylistArea
    {
        public int AreaId { get; set; }
        public int[] PlaylistIds { get; set; }
    }

    public class PlaylistAreaObj
    {
        public int ScenarioId { get; set; }
        public PlaylistArea[] PlaylistAreaArr { get; set; }
    }

    public class ScenarioRefVM
    {
        public int ScenarioId { get; set; }
        public string Title { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class ScenarioItemVM
    {
        public int ScenarioItemId { get; set; }

        public int PlaylistId { get; set; }

        public int DisplayOrder { get; set; }

        public int AreaId { get; set; }

        public int ScenarioId { get; set; }

        public string Title { get; set; }

        public string Duration { get; set; }

        public string Note { get; set; }
    }
}
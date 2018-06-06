using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.Campaing
{
    public class ScheduleModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }

        public int priority { get; set; }

        public string StatusColor { get; set; }
        public string StatusString { get; set; }

        public string ClassName { get; set; }

        public int AdType { get; set; }
        //public string Link { get; set; }
        public int DeviceType { get; set; }

        public bool AllDay { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.Campaing
{
    public class CampaingModel
    {
        public int AdId { get; set; }
        public int LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DayParting { get; set; }
        public int HourParting { get; set; }
        public int Priority { get; set; }
        public int DeviceOS { get; set; } 
    }
}
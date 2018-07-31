using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class AdvancedScheduleVM
    {
        public int? ScheduleID { get; set; }
        public int ScenarioID { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Please select device.")]
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string ScenarioTitle { get; set; }
        public int Priority { get; set; }
        public int DayFilterPoint { get; set; }
        public int TimeFilterPoint { get; set; }
    }

    public class AdvancedScheduleAddVM
    {
        public int? ScheduleID { get; set; }
        public int ScenarioID { get; set; }
        public int? LayoutID { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Please select device.")]
        public int DeviceID { get; set; }
        public bool isHorizontal { get; set; }
        public int Priority { get; set; }
        public int[] DayFilter { get; set; }
        public int[] TimeFilter { get; set; }
        public int DayFilterPoint { get; set; }
        public int TimeFilterPoint { get; set; }
    }
}
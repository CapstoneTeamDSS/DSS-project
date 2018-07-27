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
        public int? TimeToPlay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class AdvancedScheduleAddVM
    {
        public int? DeviceScenarioId { get; set; }
        public int ScenarioID { get; set; }
        public int? LayoutID { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Please select device.")]
        public int DeviceID { get; set; }
        public bool isHorizontal { get; set; }
        public bool isFixed { get; set; }
        public int? TimeToPlay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
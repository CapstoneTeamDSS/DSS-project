using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class ScheduleVM
    {
        public int? DeviceScenarioId { get; set; }
        public int ScenarioID { get; set; }
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string ScenarioTitle { get; set; }
        public int? TimeToPlay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class ScheduleAddVM
    {
        public int? DeviceScenarioId { get; set; }
        public int ScenarioID { get; set; }
        public int DeviceID { get; set; }
        public bool isHorizontal { get; set; }
        public bool isFixed { get; set; }
        public int? TimeToPlay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
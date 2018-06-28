using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class MatchingDeviceVM
    {
        public int? DeviceId { get; set; }
        [Required(ErrorMessage = "Please enter Location")]
        public int ScreenId { get; set; }
        public int BoxId { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Title")]
        public string Title { get; set; }
        public string BoxName { get; set; }
        public string ScreenName { get; set; }
        public DateTime CreateTime { get; set; }

    }
    public class MatchingDeviceBoxScreenVM
    {
        public int ScreenId { get; set; }
        public int BoxId { get; set; }
    }


}
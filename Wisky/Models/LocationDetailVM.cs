using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class LocationDetailVM
    {
        public int LocationId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Province can not Empty!!!")]
        public string Province { get; set; }
        [Required(ErrorMessage = "District can not Empty!!!")]
        public string District { get; set; }
        [Required(ErrorMessage = "Address can not Empty!!!")]
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
    public class LocationAdditionalVM
    {
        public int LocationId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Province can not Empty!!!")]
        public string Province { get; set; }
        [Required(ErrorMessage = "District can not Empty!!!")]
        public string District { get; set; }
        [Required(ErrorMessage = "Address can not Empty!!!")]
        public string Address { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
    }

    public class LocationStringVM
    {
        public int LocationId { get; set; }
        public string LocationStringList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class LocationDetailVM
    {
        public int? LocationId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please input province.")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Please input district.")]
        public string District { get; set; }
        [Required(ErrorMessage = "Please input address.")]
        public string Address { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
    public class LocationAdditionalVM
    {
        public int LocationId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Province can not be empty!!!")]
        public string Province { get; set; }
        [Required(ErrorMessage = "District can not be empty!!!")]
        public string District { get; set; }
        [Required(ErrorMessage = "Address can not be empty!!!")]
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
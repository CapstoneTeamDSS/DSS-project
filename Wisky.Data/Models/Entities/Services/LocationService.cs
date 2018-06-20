using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class LocationService
    {
        DSS.Data.Models.Entities.Repositories.ILocationRepository
            locationRepository = DependencyUtils
                .Resolve<Repositories.ILocationRepository>();

        public List<Location> GetLocationIdByBrandId(int brandId)
        {
            List<Location> result = null;
            result = locationRepository
                .Get(a => a.BrandID == brandId)
                .ToList();
            return result;
        }

        public Location GetById(int Id)
        {
            var location = this.repository
                .Get(a => a.LocationID == Id)
                .FirstOrDefault();
            return location;
        }
    }
    public partial interface ILocationService
    {
        List<Location> GetLocationIdByBrandId(int locationId);
        Location GetById(int Id);
    }
}

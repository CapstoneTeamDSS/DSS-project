using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class DeviceScenarioService
    {
        DSS.Data.Models.Entities.Repositories.IDeviceScenarioRepository
            deviceScenarioRepository = DependencyUtils
                .Resolve<Repositories.IDeviceScenarioRepository>();

        public List<DeviceScenario> GetSchedulesByBrandID(int brandId)
        {
            List<DeviceScenario> result = null;
            result = this.repository
                .Get(a => a.Device.Box.Location.BrandID == brandId && a.Device.Screen.Location.BrandID == brandId)
                .ToList();
            return result;
        }
    }

    public partial interface IDeviceScenarioService
    {
        List<DeviceScenario> GetSchedulesByBrandID(int brandId);
    }
}

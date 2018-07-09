using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial interface IDeviceService
    {
        string GetDeviceNameByID(int id);
        List<Device> GetDeviceByBrandId(int brandId);
        List<Device> GetDeviceByBrandIdAndScreenType(int brandId, bool isHorizontal);
    }

    public partial class DeviceService
    {
        DSS.Data.Models.Entities.Repositories.IDeviceRepository
            deviceRepository = DependencyUtils
                .Resolve<Repositories.IDeviceRepository>();

        public List<Device> GetDeviceByBrandId(int brandId)
        {
            List<Device> result = null;
            result = this.deviceRepository
                .Get(a => a.Screen.Location.BrandID == brandId && a.Box.Location.BrandID == brandId)
                .ToList();
            return result;
        }

        public List<Device> GetDeviceByBrandIdAndScreenType(int brandId, bool isHorizontal)
        {
            List<Device> result = null;
            result = this.deviceRepository
                .Get(a => a.Screen.Location.BrandID == brandId && a.Screen.isHorizontal == isHorizontal && a.Box.Location.BrandID == brandId)
                .ToList();
            return result;
        }

        public string GetDeviceNameByID(int id)
        {
            string result = "";
            result = this.repository
                .Get(a => a.DeviceID == id)
                .FirstOrDefault()
                ?.Title; //có thể trả ra giá trị null nếu id ko tồn tại
            return result;
        }
    }
}

using SkyWeb.DatVM.Mvc.Autofac;
using System.Collections.Generic;
using System.Linq;

namespace DSS.Data.Models.Entities.Services
{
    public partial class BrandService
    {
        DSS.Data.Models.Entities.Repositories.IDeviceRepository
            deviceRepository = DependencyUtils
                .Resolve<Repositories.IDeviceRepository>();

        public string GetBrandNameByID(int brandId)
        {
            string result = "";
            result = this.repository
                .Get(a => a.BrandID == brandId)
                .FirstOrDefault()
                ?.BrandName; //có thể trả ra giá trị null nếu brandId ko tồn tại
            return result;
        }

        public List<ViewModels.BrandViewModel> GetBrandList()//HOW TO USE DSS.Models.BrandDetailMV
        {
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var brands = brandService.Get().ToList();
            var brandVMs = new List<ViewModels.BrandViewModel>();

            foreach (var item in brands)
            {
                var b = new ViewModels.BrandViewModel
                {
                    BrandName = item.BrandName,
                    Description = item.Description,
                    BrandID = item.BrandID,
                };
                brandVMs.Add(b);
            }
            return brandVMs;
        }

        public List<Brand> GetBrandByName(string name)
        {
            name = (name ?? "").ToLower();
            var rs = new List<Brand>();
            rs = this.repository
                .Get(a => a.BrandName.Contains(name))
                .ToList();
            return rs;
        }

        public List<Device> GetDeviceByBrandName(string name)
        {
            var devices = new List<Device>();
            devices = deviceRepository
                .Get(a => a.Screen.Location.Brand.BrandName == name)
                .ToList();
            return devices;
        }
    }

    public partial interface IBrandService
    {
        List<Brand> GetBrandByName(string name);
        string GetBrandNameByID(int brandId);
        List<ViewModels.BrandViewModel> GetBrandList();
    }
}

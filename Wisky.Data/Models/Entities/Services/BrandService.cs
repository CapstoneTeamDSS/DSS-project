﻿using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class BrandService
    {
        DSS.Data.Models.Entities.Repositories.IDeviceRepository
            deviceRepository = DependencyUtils
                .Resolve<Repositories.IDeviceRepository>();

        public List<Brand> GetBrandByName(string name)
        {
            name = (name ?? "").ToLower();
            var rs = new List<Brand>();
            rs = this.repository
                .Get(a => a.BrandName.Contains(name))
                .ToList();
           return rs;
        }

        public string GetBrandNameByID(int brandId)
        {
            string result = "";
            result = this.repository
                .Get(a => a.BrandID == brandId)
                .FirstOrDefault()
                ?.BrandName;//có thể trả ra giá trị null nếu brandId ko tồn tại
            return result;
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
    }
}

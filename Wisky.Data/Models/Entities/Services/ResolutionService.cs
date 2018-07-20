using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class ResolutionService 
    {
        DSS.Data.Models.Entities.Repositories.IResolutionRepository
            resolutionRepository = DependencyUtils
                .Resolve<Repositories.IResolutionRepository>();
        public List<Resolution> GetResolutionIdByBrandId(int brandId)
        {
            List<Resolution> result = null;
            result = resolutionRepository
                .Get(a => a.BrandID == brandId)
                .ToList();
            return result;
        }
        public Resolution GetById(int Id)
        {
            var resolution = this.repository
                .Get(a => a.ResolutionID == Id)
                .FirstOrDefault();
            return resolution;
        }
    }
    public partial interface IResolutionService
    {
        List<Resolution> GetResolutionIdByBrandId(int brandId);
        Resolution GetById(int Id);
    }
}

using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class BoxService
    {
        DSS.Data.Models.Entities.Repositories.IBoxRepository
            boxRepository = DependencyUtils
                .Resolve<Repositories.IBoxRepository>();

        public List<Box> GetBoxIdByBrandId(int brandId)
        {
            List<Box> result = null;
            result = boxRepository
                .Get(a => a.Location.BrandID == brandId)
                .ToList();
            return result;
        }
        public string GetBoxNameByID(int boxId)
        {
            string result = "";
            result = this.repository
                .Get(a => a.BoxID == boxId)
                .FirstOrDefault()
                ?.BoxName; 
            return result;
        }
        public Box GetById(int Id)
        {
            var box = this.repository
                .Get(a => a.BoxID == Id)
                .FirstOrDefault();
            return box;
        }
    }
    public partial interface IBoxService
    {
        List<Box> GetBoxIdByBrandId(int brandId);
        Box GetById(int Id);
        string GetBoxNameByID(int boxId);
    }
}

using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class ScreenService
    {
        DSS.Data.Models.Entities.Repositories.IScreenRepository
            screenRepository = DependencyUtils
                .Resolve<Repositories.IScreenRepository>();

        public List<Screen> GetScreenIdByBrandId(int brandId)
        {
            List<Screen> result = null;
            result = screenRepository
                .Get(a => a.Location.BrandID == brandId)
                .ToList();
            return result;
        }
        public string GetScreenNameByID(int screenId)
        {
            string result = "";
            result = this.repository
                .Get(a => a.ScreenID == screenId)
                .FirstOrDefault()
                ?.ScreenName; 
            return result;
        }

        public Screen GetById(int Id)
        {
            var screen = this.repository
                .Get(a => a.ScreenID == Id)
                .FirstOrDefault();
            return screen;
        }
    }
    public partial interface IScreenService
    {
        List<Screen> GetScreenIdByBrandId(int brandId);
        Screen GetById(int Id);
        string GetScreenNameByID(int screenId);
    }
}

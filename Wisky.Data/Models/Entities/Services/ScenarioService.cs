using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class ScenarioService
    {
        DSS.Data.Models.Entities.Repositories.IScenarioRepository
            scenarioRepository = DependencyUtils
                .Resolve<Repositories.IScenarioRepository>();

        public List<Scenario> GetScenarioIdByBrandId(int brandId)
        {
            List<Scenario> result = null;
            result = scenarioRepository
                .Get(a => a.BrandID == brandId)
                .ToList();
            return result;
        }

        public List<Scenario> GetScenarioIdByBrandIdAndLayoutType(int brandId, bool isHorizontal)
        {
            List<Scenario> result = null;
            result = scenarioRepository
                .Get(a => a.BrandID == brandId && a.Layout.isHorizontal == isHorizontal)
                .ToList();
            return result;
        }

        public Scenario GetById(int Id)
        {
            var scenario = this.repository
                .Get(a => a.ScenarioID == Id)
                .FirstOrDefault();
            return scenario;
        }

        public String GetScenarioNameById(int Id)
        {
            string result = "";
            result = this.repository
                .Get(a => a.ScenarioID == Id)
                .FirstOrDefault()
                ?.Title; //có thể trả ra giá trị null nếu id ko tồn tại
            return result;
        }

        public int GetLayoutIDById(int Id)
        {
            int result;
            result = this.repository
                .Get(a => a.ScenarioID == Id)
                .FirstOrDefault()
                ?.LayoutID ?? -1;
            return result;
        }

        public bool? GetScenarioOrientationById(int Id)
        {
            bool? result = true;
            result = this.repository
                .Get(a => a.ScenarioID == Id)
                .FirstOrDefault()
                ?.Layout.isHorizontal; //có thể trả ra giá trị null nếu id ko tồn tại
            return result;
        }
    }

    public partial interface IScenarioService
    {
        List<Scenario> GetScenarioIdByBrandId(int brandId);
        List<Scenario> GetScenarioIdByBrandIdAndLayoutType(int brandId, bool isHorizontal);
        Scenario GetById(int Id);
        String GetScenarioNameById(int Id);
        bool? GetScenarioOrientationById(int Id);
        int GetLayoutIDById(int Id);
    }
}

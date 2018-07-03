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

        public Scenario GetById(int Id)
        {
            var scenario = this.repository
                .Get(a => a.ScenarioID == Id)
                .FirstOrDefault();
            return scenario;
        }
    }

    public partial interface IScenarioService
    {
        List<Scenario> GetScenarioIdByBrandId(int brandId);
        Scenario GetById(int Id);
    }
}

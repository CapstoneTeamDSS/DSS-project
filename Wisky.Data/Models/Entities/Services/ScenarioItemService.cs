using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class ScenarioItemService
    {
        DSS.Data.Models.Entities.Repositories.IScenarioItemRepository
           scenarioItemRepository = SkyWeb.DatVM.Mvc.Autofac.DependencyUtils
               .Resolve<Repositories.IScenarioItemRepository>();

        public List<ScenarioItem> GetItemListByAreaScenarioId(int areaId, int scenarioId)
        {
            var devices = new List<ScenarioItem>();
            devices = scenarioItemRepository
                .Get(a => a.AreaID == areaId && a.ScenarioID == scenarioId)
                .OrderBy(a => a.DisplayOrder)
                .ToList();
            return devices;
        }

        public bool CheckIfPlaylistAdded(int areaId, int scenarioId, int playlistId)
        {
            var devices = scenarioItemRepository
                .Get(a => a.AreaID == areaId && a.ScenarioID == scenarioId && a.PlaylistID == playlistId)
                .FirstOrDefault();
            return devices==null;
        }
    }

    public partial interface IScenarioItemService
    {
        List<ScenarioItem> GetItemListByAreaScenarioId(int areaId, int scenarioId);
        bool CheckIfPlaylistAdded(int areaId, int scenarioId, int playlistId);

    }
}

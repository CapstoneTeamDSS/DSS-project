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
            var items = new List<ScenarioItem>();
            items = scenarioItemRepository
                .Get(a => a.AreaID == areaId && a.ScenarioID == scenarioId)
                .OrderBy(a => a.DisplayOrder)
                .ToList();
            return items;
        }

        public bool CheckIfPlaylistAdded(int areaId, int scenarioId, int playlistId)
        {
            var item = scenarioItemRepository
                .Get(a => a.AreaID == areaId && a.ScenarioID == scenarioId && a.PlaylistID == playlistId)
                .FirstOrDefault();
            return item==null;
        }

        public List<ScenarioItem> GetItemListByScenarioId(int scenarioId)
        {
            var items = new List<ScenarioItem>();
            items = scenarioItemRepository
                .Get(a => a.ScenarioID == scenarioId)
                .OrderBy(a => a.DisplayOrder)
                .ToList();
            return items;
        }
    }

    public partial interface IScenarioItemService
    {
        List<ScenarioItem> GetItemListByAreaScenarioId(int areaId, int scenarioId);
        bool CheckIfPlaylistAdded(int areaId, int scenarioId, int playlistId);
        List<ScenarioItem> GetItemListByScenarioId(int scenarioId);
    }
}

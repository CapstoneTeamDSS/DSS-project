using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class PlaylistService
    {
        DSS.Data.Models.Entities.Repositories.IPlaylistRepository
            playListRepository = DependencyUtils
                .Resolve<Repositories.IPlaylistRepository>();

        public List<Playlist> GetPlaylistIdByBrandId(int brandId)
        {
            List<Playlist> result = null;
            result = playListRepository
                .Get(a => a.BrandID == brandId)
                .ToList();
            return result;
        }

        public Playlist GetById(int Id)
        {
            var playList = this.repository
                .Get(a => a.PlaylistID == Id)
                .FirstOrDefault();
            return playList;
        }
    }
    public partial interface IPlaylistService
    {
        List<Playlist> GetPlaylistIdByBrandId(int brandId);
        Playlist GetById(int Id);
    }
}

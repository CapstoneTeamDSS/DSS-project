using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class PlaylistItemService
    {
        DSS.Data.Models.Entities.Repositories.IPlaylistItemRepository
            playlistItemRepository = DependencyUtils
                .Resolve<Repositories.IPlaylistItemRepository>();

        public List<PlaylistItem> GetMediaSrcByPlaylistId(int playlistId)
        {
            List<PlaylistItem> result = null;
            result = playlistItemRepository
                .Get(a => a.PlaylistID == playlistId)
                .OrderBy(a => a.DisplayOrder)
                .ToList();
            return result;
        }

        public PlaylistItem GetPlaylistItemById(int id)
        {
            PlaylistItem playlistItem = playlistItemRepository
                .Get(a => a.PlaylistItemID == id)
                .FirstOrDefault();
            return playlistItem;
        }
    }

    public partial interface IPlaylistItemService
    {
        List<PlaylistItem> GetMediaSrcByPlaylistId(int playlistId);
        PlaylistItem GetPlaylistItemById(int id);
    }
}

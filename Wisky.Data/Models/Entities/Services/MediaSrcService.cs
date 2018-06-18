using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial class MediaSrcService
    {
        DSS.Data.Models.Entities.Repositories.IMediaSrcRepository
            mediaSrcRepository = DependencyUtils
                .Resolve<Repositories.IMediaSrcRepository>();

        public List<MediaSrc> GetMediaSrcByBrand(int brandId)
        {
            List<MediaSrc> result = null;
            result = mediaSrcRepository
                .Get(a => a.BrandID == brandId)
                .ToList();
            return result;
        }
    }

    public partial interface IMediaSrcService
    {
        List<MediaSrc> GetMediaSrcByBrand(int brandId);
    }
}
